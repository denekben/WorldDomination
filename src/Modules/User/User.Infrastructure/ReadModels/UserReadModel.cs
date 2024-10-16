namespace User.Infrastructure.ReadModels
{
    public class UserReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio {  get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileImagePath { get; set; }
        public string DefaultProfileImagePath { get; set; }
        public UserStatusReadModel UserStatusReadModel { get; set; }
        public ICollection<UserAchievmentReadModel>? UserAchievmentsReadModel { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
