using User.Domain.Entities.Relationships;
using WorldDomination.Shared.Domain;

namespace User.Domain.Entities
{
    public sealed class Achievment
    {
        public IdValueObject Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime? CreatedTime { get; private set; }
        public DateTime? UpdatedTime { get; private set; }
        public ICollection<UserAchievment>? UserAchievments { get; private set; }

        // EF
        private Achievment() { }

        private Achievment(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public static Achievment Create(
            string name, string description)
        {
            return new Achievment(name, description);
        }
    }
}
