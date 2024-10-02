namespace AppUser.Infrastructure.DomainUser.ReadModels
{
    public class ActivityStatusReadModel
    {
        public Guid UserId { get; set; }
        public string IsInGameStatus { get; set; }
        public string? Country { get; set; }
        public int? RoundNumber { get; set; }
        public string? GameRole { get; set; }
        public UserReadModel UserReadModel { get; set; }
    }
}
