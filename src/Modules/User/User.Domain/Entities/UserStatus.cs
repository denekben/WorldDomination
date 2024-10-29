using User.Domain.ValueObjects;
using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Entities
{
    public sealed class UserStatus : DomainEntity 
    {
        public IdValueObject UserId { get; private set; }
        public ActivityStatus ActivityStatus { get; private set; }
        public string? Country { get; private set; }
        public int? RoundNumber { get; private set; }
        public string? GameRole { get; private set; }
        public DomainUser User { get; private set; }

        // EF
        private UserStatus() { }

        private UserStatus(Guid userId)
        {
            UserId = userId;
            ActivityStatus = ActivityStatus.Online;
            Country = null;
            RoundNumber = null;
            GameRole = null;
        }

        public static UserStatus Create(Guid userId)
        {
            return new UserStatus(userId);
        }

        public override string ToString()
        {
            return $"ActivityStatus {{ UserId = {UserId}, Status = {ActivityStatus}, Country = {Country}, RoundNumber = {RoundNumber}, GameRole = {GameRole} }}";
        }

        public void ChangeActivityStatus(ActivityStatus activityStatus)
        {
            ActivityStatus = activityStatus;
        }
    }
}
