using AppUser.Domain.Entities;
using AppUser.Domain.Entities.Relationships;
using AppUser.Domain.ValueObjects;
using WorldDomination.Shared.Domain;

namespace UserAccess.Domain.Entities
{
    public sealed class User
    {
        public IdValueObject Id { get; private set; }
        public Username Username { get; private set; }
        public Email Email { get; private set; }
        public string ProfileImagePath { get; private set; }
        public ActivityStatus ActivityStatus { get; private set; }
        public ICollection<UserAchievment> UserAchievments { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime UpdatedTime { get; private set; }

        // EF
        private User() { }

        private User(
            string id,
            string name,
            string email,
            string profileImagePath)
        {
            Id = new Guid(id);
            Username = name;
            Email = email;
            ProfileImagePath = profileImagePath;
            CreatedTime = DateTime.UtcNow;
        }

        public static User CreateUser(
            string id,
            string name,
            string email,
            string profileImagePath = "")
        {
            if(string.IsNullOrEmpty(profileImagePath))
            {
                profileImagePath = GenerateRandomCode().ToString();
            }
            return new User(id, name, email, profileImagePath );
        }

        public void ChangeUsername(string username)
        {
            Username = username;
        }

        public void ChangeProfileImagePath(string path)
        {
            ProfileImagePath = path;
        }

        private static int GenerateRandomCode()
        {
            Random _random = new Random();
            return _random.Next(10);
        }
    }
}
