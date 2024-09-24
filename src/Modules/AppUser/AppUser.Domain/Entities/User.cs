using AppUser.Domain.ValueObjects;
using UserAccess.Domain.ValueObjects;
using WorldDomination.Shared.Domain;

namespace UserAccess.Domain.Entities
{
    public sealed class User 
    {
        public IdValueObject Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public IReadOnlyList<UserRole> Roles { get => _roles; }
        public UserActivityStatus ActivityStatus { get; private set; }
        public IReadOnlyList<UserAchievment> Achievments { get => _achievments; }
        public DateTime CreatedTime { get; private set; }
        public DateTime UpdatedTime { get; private set; }

        private List<UserRole> _roles = new();
        private List<UserAchievment> _achievments = new();

        // EF
        private User() { }

        public static User CreateMember(
            string id,
            string name,
            string email)
        {
            return new User(id, name, email, UserRole.Member);
        }

        public static User CreateAdmin(
            string id,
            string name,
            string email)
        {
            return new User(id, name, email, UserRole.Admin);
        }

        private User(
            string id,
            string name,
            string email,
            UserRole role)
        {
            Id = new Guid(id);
            Username = name;
            Email = email;
            _roles.Add(role);
            CreatedTime = DateTime.Now;
        }
    }
}
