namespace AppUser.Infrastructure.DomainUser.ReadModels
{
    public class AchievmentReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserAchievmentReadModel>? UserAchievments { get; set; }
    }
}
