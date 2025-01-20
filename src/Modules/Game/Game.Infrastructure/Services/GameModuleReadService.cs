using Game.Application.Services;
using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Domain.DomainModels.ReadModels.Users;
using Game.Domain.ReadModels.Messaging;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Services
{
    public class GameModuleReadService : IGameModuleReadService
    {
        private readonly GameReadDbContext _context;

        public GameModuleReadService(GameReadDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CountryExistsByIdAsync(Guid countryId)
        {
            return await _context.Countries.AnyAsync(c=>c.Id == countryId);
        }

        public async Task<bool> CountryExistsByNormalizedNameAsync(Guid roomId, string normalizedName)
        {
            return await _context.Countries.AnyAsync(c=>c.NormalizedName == normalizedName && c.RoomId == roomId);
        }

        public async Task<bool> GameExistsByRoomIdAsync(Guid roomId)
        {
            return await _context.Games.AnyAsync(g=>g.RoomId == roomId);
        }

        public async Task<NegotiationRequestReadModel?> GetNegotiationRequestByChannelAsync(Guid firstCountryId, Guid secondCountryId)
        {
            return await _context.NegotiationRequests.FirstOrDefaultAsync(nr=>
            (nr.IssuerCountryId == firstCountryId && nr.AudienceCountryId == secondCountryId && nr.IsApplied)
            || (nr.IssuerCountryId == secondCountryId && nr.AudienceCountryId == firstCountryId));
        }

        public async Task<RoomMemberReadModel?> GetRoomMemberAsync(Guid memberId, Guid roomId)
        {
            return await _context.RoomMembers.FirstOrDefaultAsync(m=>m.GameUserId == memberId && m.RoomId == roomId);
        }

        public async Task<GameUserReadModel?> GetUserAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Id == userId);
        }

        public async Task<bool> NegotationChannelExists(Guid firstCountryId, Guid secondCountryId)
        {
            return await _context.NegotiationRequests.AnyAsync(nr=>
            (nr.IssuerCountryId== firstCountryId && nr.AudienceCountryId== secondCountryId && nr.IsApplied)
            || (nr.IssuerCountryId == secondCountryId && nr.AudienceCountryId == firstCountryId));
        }

        public async Task<bool> RoomMemberExistsByUserIdAsync(Guid userId)
        {
            return await _context.RoomMembers.AnyAsync(m=>m.GameUserId == userId);
        }
    }
}
