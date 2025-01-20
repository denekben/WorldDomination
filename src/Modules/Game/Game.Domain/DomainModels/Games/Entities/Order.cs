using Game.Domain.Interfaces.Countries;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        public Dictionary<IdValueObject, int> CountriesToDonate { get; private set; }

        public Country Country { get; private set; }
        public IdValueObject RoomId { get; private set; }

        //EF
        private Order() { }

        private Order(List<Guid> citiesToDevelop, List<Guid> citiesToSetShield,
            bool developEcologyProgram, bool developNuclearTechology,
            int bombsToBuyQuantity, List<Guid> citiesToStrike, 
            List<Guid> countriesToSetSanctions, Dictionary<Guid, int> countriesToDonate, Guid countryId, Guid roomId)
        {
            CitiesToDevelop = citiesToDevelop.GuidsToVO();
            CitiesToSetShield = citiesToSetShield.GuidsToVO();
            DevelopEcologyProgram = developEcologyProgram;
            DevelopNuclearTechology = developNuclearTechology;
            BombsToBuyQuantity = bombsToBuyQuantity;
            CitiesToStrike = citiesToStrike.GuidsToVO();
            CountriesToSetSanctions = countriesToSetSanctions.GuidsToVO();
            CountriesToDonate = countriesToDonate.GuidsToVO();
            CountryId = countryId;
            RoomId = roomId;
        }

        public static Order Create(Guid countryId, List<Guid> citiesToDevelop, List<Guid> citiesToSetShield, 
            bool developEcologyProgram, bool developNuclearTechology, 
            int bombsToBuyQuantity, List<Guid> citiesToStrike, 
            List<Guid> countriesToSetSanctions, Dictionary<Guid, int> countriesToDonate, Guid roomId)
        {
            return new(
                citiesToDevelop,
                citiesToSetShield,
                developEcologyProgram,
                developNuclearTechology,
                bombsToBuyQuantity,
                citiesToStrike,
                countriesToSetSanctions,
                countriesToDonate,
                countryId,
                roomId
            );
        }

        public int CalculateTotalCost(ICountryStrategy strategy)
        {
            return (int) CitiesToDevelop.Count * strategy.CityDevelopmentCost
                + CountriesToDonate.Select(ctd=>ctd.Value).Sum()
                + CitiesToSetShield.Count * strategy.ShieldCost
                + (DevelopEcologyProgram ? strategy.EcologyDevelopmentCost : 0)
                + (DevelopNuclearTechology ? strategy.NuclearTechnologyCost : 0)
                + BombsToBuyQuantity * strategy.BombCost;
        }
    }
}
