using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly GameWriteDbContext _context;

        public ChatRepository(GameWriteDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
