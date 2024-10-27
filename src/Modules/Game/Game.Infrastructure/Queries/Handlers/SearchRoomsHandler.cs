using Game.Application.Rooms.Queries;
using Game.Infrastructure.Contexts;
using Game.Infrastructure.Mappers;
using Game.Domain.ReadModels.RoomAggregate;
using Game.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Services;

namespace Game.Infrastructure.Queries.Handlers
{
    internal class SearchRoomsHandler : IRequestHandler<SearchRooms, List<RoomDto>>
    {
        private readonly DbSet<RoomReadModel> _rooms;
        private readonly IHttpContextService _contextService;

        public SearchRoomsHandler(GameReadDbContext context, IHttpContextService contextService)
        {
            _rooms = context.Rooms;
            _contextService = contextService;
        }

        public async Task<List<RoomDto>> Handle(SearchRooms query, CancellationToken cancellationToken)
        {
            var rooms = _rooms.Where(r=> EF.Functions.ILike(r.RoomName, $"%{query.SearchPhrase ?? string.Empty}%"));

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            Guid userId;
            try
            {
                userId = _contextService.GetCurrentUserId();
            }
            catch (Exception)
            {
                return await rooms.Skip(skipNumber).Take(query.PageSize).Select(r => r.AsRoomDto()).ToListAsync();
            }

            return await rooms.Skip(skipNumber).Take(query.PageSize).OrderByDescending(r=>r.CreatorId == userId).Select(r => r.AsRoomDto()).ToListAsync();
        }
    }
}
