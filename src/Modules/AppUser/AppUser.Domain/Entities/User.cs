using UserAccess.Domain.ValueObjects;
using WorldDomination.Shared.Domain;

namespace UserAccess.Domain.Entities
{
    public class User
    {
        public IdValueObject Id { get; private set; }
        public UserName Username { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public IReadOnlyList<UserRole> Roles
        {
            get => _roles;
            private set => _roles.Add((UserRole)value);
        }

        private List<UserRole> _roles = new();

        // EF
        private User() { }

        public static User CreateMember(
            string name)
        {
            return new User(name, UserRole.Member);
        }

        public static User CreateAdmin(
            string name)
        {
            return new User(name, UserRole.Admin);
        }

        private User(
           string name,
           UserRole role)
        {
            Username = name;
            _roles.Add(role);
            CreatedTime = DateTime.Now;
        }
    }
}
