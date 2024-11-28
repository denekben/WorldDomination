namespace Game.Application.Services
{
    public interface IGameTimerService
    {
        void AddGame(Guid roomId, int? intervalInMilliseconds = null);
        void RemoveGame(Guid roomId);
    }
}
