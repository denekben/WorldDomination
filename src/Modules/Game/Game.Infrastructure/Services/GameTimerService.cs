using System.Collections.Concurrent;
using Game.Application.Services;
using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

public class GameTimerService : IHostedService, IDisposable, IGameTimerService
{
    private const int _debatesInterval = 120_000;
    private const int _negotiationsInterval = 300_000;
    private const int _orderMakingInterval = 60_000;

    private readonly ILogger<GameTimerService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ConcurrentDictionary<Guid, Timer> _gameTimers = new ConcurrentDictionary<Guid, Timer>();
    private bool _isRunning;

    public GameTimerService(ILogger<GameTimerService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _isRunning = true;
        _logger.LogInformation("Game Timer Service is starting.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _isRunning = false;

        foreach (var timer in _gameTimers.Values)
        {
            timer.Dispose();
        }

        _logger.LogInformation("Game Timer Service is stopping.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        StopAsync(CancellationToken.None).Wait();
    }

    public void AddGame(Guid gameId, int? intervalInMilliseconds)
    {
        if (_gameTimers.ContainsKey(gameId))
            return; 

        intervalInMilliseconds ??= _debatesInterval;

        var timer = new Timer(OnTimerElapsed, gameId, (int)intervalInMilliseconds, Timeout.Infinite);
        _gameTimers[gameId] = timer;
        _logger.LogInformation($"Added game with ID {gameId} to timer service.");
    }

    public void UpdateGameTimer(Guid gameId, int intervalInMilliseconds)
    {
        if (_gameTimers.TryGetValue(gameId, out var existingTimer))
        {
            existingTimer.Dispose();
        }

        var timer = new Timer(OnTimerElapsed, gameId, intervalInMilliseconds, Timeout.Infinite);
        _gameTimers[gameId] = timer;
        _logger.LogInformation($"Updated game with ID {gameId} to timer service.");
    }

    public void RemoveGame(Guid gameId)
    {
        if (_gameTimers.TryRemove(gameId, out var timer))
        {
            timer.Dispose();
            _logger.LogInformation($"Removed game with ID {gameId} from timer service.");
        }
    }

    private async void OnTimerElapsed(object state)
    {
        var gameId = (Guid)state;

        using (var scope = _serviceScopeFactory.CreateScope()) 
        {
            var gameRepository = scope.ServiceProvider.GetRequiredService<IGameRepository>(); 
            var notifications = scope.ServiceProvider.GetRequiredService<IGameModuleNotificationService>();

            var game = await gameRepository.GetAsync(gameId, GameIncludes.CountriesWithCitiesWithOrders)
            ?? throw new BadRequestException($"Cannot find Game {gameId}");

            game.ChangeState();

            if (game.GameState == GameState.Debates)
            {
                foreach (var country in game.Countries)
                {
                    if (country.HasValidatedOrder)
                        country.ApplyOrder(country.Order, game.Countries, game);
                }
                foreach (var country in game.Countries)
                {
                    country.UpdateState(game.EcologyLevel);
                }
            }

            await gameRepository.UpdateAsync(game);

            if (game.CurrentRound > game.RoundQuantity)
            {
                RemoveGame(gameId);
                await notifications.GameEnded(gameId);
                _logger.LogInformation($"Game {game.RoomId} ended");
                return;
            }

            _logger.LogInformation($"Game {game.RoomId} changed state to {game.GameState}");

            await notifications.GameStateChanged(game.GameState, game.RoomId);

            if (game.GameState == GameState.Debates)
                UpdateGameTimer(gameId, _debatesInterval);
            else if (game.GameState == GameState.Negotiations)
                UpdateGameTimer(gameId, _negotiationsInterval);
            else
                UpdateGameTimer(gameId, _orderMakingInterval);
        }
    }
}