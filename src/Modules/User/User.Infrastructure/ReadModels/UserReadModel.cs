namespace User.Infrastructure.ReadModels
{
    public sealed class UserReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Bio {  get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string ProfileImagePath { get; private set; }
        public string DefaultProfileImagePath { get; private set; }
        public UserStatusReadModel UserStatus { get; private set; }
        public ICollection<UserAchievmentReadModel>? UserAchievments { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? UpdatedTime { get; private set; }
    }
}
