namespace AppUser.Infrastructure.DomainUser.ReadModels
{
    internal class ActivityStatusReadModel
    {
        public Guid UserId { get; set; }
        public string IsInGameStatus { get; set; }
        public string Country { get; set; }
        public string RoundNumber { get; set; }
        public string GameRole { get; set; }
    }
}
