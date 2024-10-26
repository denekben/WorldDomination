namespace User.Infrastructure.ReadModels
{
    public sealed class AchievmentReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? UpdatedTime { get; private set; }
        public ICollection<UserAchievmentReadModel>? UserAchievments { get; private set; }
    }
}
