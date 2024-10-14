using User.Domain.ValueObjects;
using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Infrastructure.ReadModels
{
    public class UserStatusReadModel
    {
        public Guid UserId { get; set; }
        public string ActivityStatus { get; set; }
        public string? Country { get; set; }
        public int? RoundNumber { get; set; }
        public string? GameRole { get; set; }
        public UserReadModel UserReadModel { get; set; }
    }
}