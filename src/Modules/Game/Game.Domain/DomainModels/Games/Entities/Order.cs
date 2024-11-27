using Game.Domain.Interfaces.Countries;
using WorldDomination.Shared.Domain;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class Order : DomainEntity
    {
        public IdValueObject CountryId { get; private set; }
        public List<IdValueObject> CitiesToDevelop { get; private set; }
        public List<IdValueObject> CitiesToSetShield { get; private set; }
        public bool DevelopEcologyProgram {  get; private set; }
        public bool DevelopNuclearTechology {  get; private set; }
        public int BombsToBuyQuantity { get; private set; }
        public List<IdValueObject> CitiesToStrike { get; private set; }
        public List<IdValueObject> CountriesToSetSanctions { get; private set; }

        public Country Country { get; private set; }
        public IdValueObject RoomId { get; private set; }

        //EF
        private Order() { }

        private Order(List<Guid> citiesToDevelop, List<Guid> citiesToSetShield,
            bool developEcologyProgram, bool developNuclearTechology,
            int bombsToBuyQuantity, List<Guid> citiesToStrike, 
            List<Guid> countriesToSetSanctions, Guid countryId, Guid roomId)
        {
            CitiesToDevelop = citiesToDevelop.ToIdValueObjects();
            CitiesToSetShield = citiesToSetShield.ToIdValueObjects();
            DevelopEcologyProgram = developEcologyProgram;
            DevelopNuclearTechology = developNuclearTechology;
            BombsToBuyQuantity = bombsToBuyQuantity;
            CitiesToStrike = citiesToStrike.ToIdValueObjects();
            CountriesToSetSanctions = countriesToSetSanctions.ToIdValueObjects();
            CountryId = countryId;
            RoomId = roomId;
        }

        public static Order Create(Guid countryId, List<Guid> citiesToDevelop, List<Guid> citiesToSetShield, 
            bool developEcologyProgram, bool developNuclearTechology, 
            int bombsToBuyQuantity, List<Guid> citiesToStrike, 
            List<Guid> countriesToSetSanctions, Guid roomId)
        {
            return new(
                citiesToDevelop,
                citiesToSetShield,
                developEcologyProgram,
                developNuclearTechology,
                bombsToBuyQuantity,
                citiesToStrike,
                countriesToSetSanctions,
                countryId,
                roomId
            );
        }

        public int CalculateTotalCost(ICountryStrategy strategy)
        {
            return (int) CitiesToDevelop.Count * strategy.CityDevelopmentCost
                + CitiesToSetShield.Count * strategy.ShieldCost
                + (DevelopEcologyProgram ? strategy.EcologyDevelopmentCost : 0)
                + (DevelopNuclearTechology ? strategy.NuclearTechnologyCost : 0)
                + BombsToBuyQuantity * strategy.BombCost;
        }
    }
}
