using AppUser.Domain.ValueObjects;
using WorldDomination.Shared.Domain;

namespace AppUser.Infrastructure.DomainUser.ReadModels
{
    public class UserReadModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IReadOnlyList<RoleReadModel> Roles { get; set; }
        public UserActivityStatus ActivityStatus { get; set; }
        public IReadOnlyList<UserAchievment> Achievments { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
