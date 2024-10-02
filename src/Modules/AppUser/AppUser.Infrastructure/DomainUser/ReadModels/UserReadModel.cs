namespace AppUser.Infrastructure.DomainUser.ReadModels
{
    public class UserReadModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } 
        public string ProfileImagePath { get; set; }
        public ActivityStatusReadModel? ActivityStatusReadModel { get; set; }
        public ICollection<UserAchievmentReadModel>? UserAchievments { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
