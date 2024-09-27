namespace AppUser.Infrastructure.DomainUser.ReadModels
{
    internal class UserAchievmentReadModel
    {
        public Guid UserId { get; set; }
        public Guid AchievmentId { get; set; }
        public UserReadModel UserReadModel { get; set; }
        public AchievmentReadModel AchievmentReadModel { get; set; }
        public DateTime AchievedTime { get; set; }
    }
}
