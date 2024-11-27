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

        private Order(List<IdValueObject> citiesToDevelop, List<IdValueObject> citiesToSetShield,
            bool developEcologyProgram, bool developNuclearTechology,
            int bombsToBuyQuantity, List<IdValueObject> citiesToStrike, 
            List<IdValueObject> countriesToSetSanctions, IdValueObject countryId, IdValueObject roomId)
        {
            CitiesToDevelop = citiesToDevelop;
            CitiesToSetShield = citiesToSetShield;
            DevelopEcologyProgram = developEcologyProgram;
            DevelopNuclearTechology = developNuclearTechology;
            BombsToBuyQuantity = bombsToBuyQuantity;
            CitiesToStrike = citiesToStrike;
            CountriesToSetSanctions = countriesToSetSanctions;
            CountryId = countryId;
            RoomId = roomId;
        }

        public static Order Create(List<IdValueObject> citiesToDevelop, List<IdValueObject> citiesToSetShield, 
            bool developEcologyProgram, bool developNuclearTechology, 
            int bombsToBuyQuantity, List<IdValueObject> citiesToStrike, 
            List<IdValueObject> countriesToSetSanctions, IdValueObject countryId, IdValueObject roomId)
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
