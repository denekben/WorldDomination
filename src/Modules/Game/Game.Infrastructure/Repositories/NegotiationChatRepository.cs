using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class NegotiationChatRepository : INegotiationChatRepository
    {
        private readonly GameWriteDbContext _context;

        public NegotiationChatRepository(GameWriteDbContext context)
        {
            _context = context;
        }

        public async Task<NegotiationChat?> GetAsync(IdValueObject firstCountryId, IdValueObject secondCountryId)
        {
            return await _context.NegotiationChats.FirstOrDefaultAsync(nc => nc.FirstCountryId == firstCountryId && nc.SecondCountryId == secondCountryId);
        }

        public async Task<NegotiationChat?> GetAsync(IdValueObject chatId)
        {
            return await _context.NegotiationChats.FirstOrDefaultAsync(nc => nc.Id == chatId);
        }

        public async Task UpdateAsync(NegotiationChat chat)
        {
            _context.NegotiationChats.Update(chat);
            await _context.SaveChangesAsync();
        }
    }
}
