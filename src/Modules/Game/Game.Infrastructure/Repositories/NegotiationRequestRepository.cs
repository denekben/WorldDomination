using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class NegotiationRequestRepository : INegotiationRequestRepository
    {
        private readonly GameWriteDbContext _context;

        public NegotiationRequestRepository(GameWriteDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NegotiationRequest request)
        {
            await _context.NegotiationRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(NegotiationRequest request)
        {
            _context.NegotiationRequests.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<NegotiationRequest?> GetAsync(IdValueObject issuerCountryId, IdValueObject audienceCountryId)
        {
            return await _context.NegotiationRequests.FirstOrDefaultAsync(nr=>nr.IssuerCountryId == issuerCountryId && nr.AudienceCountryId == audienceCountryId);
        }

        public async Task UpdateAsync(NegotiationRequest request)
        {
            _context.NegotiationRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
