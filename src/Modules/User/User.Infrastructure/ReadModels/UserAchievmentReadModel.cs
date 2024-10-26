namespace User.Infrastructure.ReadModels
{
    public sealed class UserAchievmentReadModel
    {
        public Guid UserId { get; private set; }
        public Guid AchievmentId { get; private set; }
        public UserReadModel User { get; private set; }
        public AchievmentReadModel Achievment { get; private set; }
        public DateTime AchievedTime { get; private set; }
    }
}
