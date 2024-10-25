using User.Domain.Entities.Relationships;
using User.Domain.ValueObjects;
using WorldDomination.Shared.Domain;

namespace User.Domain.Entities
{
    public sealed class DomainUser
    {
        public IdValueObject Id {  get; private set; }
        public string Name { get; private set; }
        public Bio Bio { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public ProfileImagePath ProfileImagePath { get; private set; }
        public DefaultProfileImagePath DefaultProfileImagePath { get; private set; }
        public UserStatus UserStatus { get; private set; }
        public ICollection<UserAchievment>? UserAchievments { get; private set; }
        public DateTime? CreatedTime { get; private set; }
        public DateTime? UpdatedTime { get; private set; }

        // EF
        private DomainUser() { }

        private DomainUser(
            string id,
            string username,
            string email
            )
        {
            Id = new Guid(id);
            Name = username;
            Bio = Bio.Create();
            Username = username;
            Email = email;
            DefaultProfileImagePath = DefaultProfileImagePath.Create();
            ProfileImagePath = ProfileImagePath.Create(DefaultProfileImagePath);
        }

        public static DomainUser Create(
            string id,
            string username,
            string email
            )
        {

            return new DomainUser(id, username, email);
        }

        public void ChangeUsername(string username)
        {
            Username = username;
        }

        public void ChangeProfileInfo(string? name, string? bio)
        {
            Name = name ?? Username;
            Bio = bio;
        }

        public void ChangeProfileImagePath(string? path = null)
        {
            ProfileImagePath = string.IsNullOrEmpty(path) ? (string) DefaultProfileImagePath : path;
        }


    }
}
