using UserAccess.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace AppUser.Domain.Entities.Relationships
{
    public sealed class UserAchievment
    {
        public IdValueObject UserId { get; private set; }
        public IdValueObject AchievmentId { get; private set; }
        public User User { get; private set; }
        public Achievment Achievment { get; private set; }
    }
}
