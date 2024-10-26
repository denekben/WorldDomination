namespace User.Infrastructure.ReadModels
{
    public sealed class UserStatusReadModel
    {
        public Guid UserId { get; private set; }
        public string ActivityStatus { get; private set; }
        public string? Country { get; private set; }
        public int? RoundNumber { get; private set; }
        public string? GameRole { get; private set; }
        public UserReadModel User { get; private set; }
    }
}