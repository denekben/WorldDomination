using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Entities.Relationships
{
    public sealed class UserAchievment
    {
        public IdValueObject UserId { get; private set; }
        public IdValueObject AchievmentId { get; private set; }
        public DomainUser User { get; private set; }
        public Achievment Achievment { get; private set; }
        public DateTime? AchievedTime { get; private set; }
    }
}
