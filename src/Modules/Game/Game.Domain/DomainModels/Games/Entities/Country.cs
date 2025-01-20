using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using Game.Domain.DomainModels.Rooms.ValueObjects;
using Game.Domain.Interfaces.Countries;
using Microsoft.Extensions.DependencyInjection;
using Game.Domain.DomainModels.Messaging.Entities;


namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class Country : DomainEntity
    {
        public ICountryStrategy _strategy;

        public IdValueObject Id { get; set; }
        public string CountryName { get; private set; }
        public string NormalizedName { get; private set; }
        public string FlagImagePath { get; private set; }
        public LivingLevel LivingLevel { get; private set; }
        public Budget Budget { get; private set; }
        public bool HaveNuclearTechnology { get; private set; }
        public NuclearTechnology NuclearTechnology { get; private set; }
        public Income Income { get; private set; }
        public bool HasAppliedOrder { get; private set; }
        public bool HasValidatedOrder { get; private set; }

        public List<RoomMember> Players { get; private set; } = [];
        public List<City> Cities { get; private set; } = [];
        public List<Sanction> OutgoingSanctions { get; private set; } = [];
        public List<Sanction> IncomingSanctions { get; private set; } = [];
        public List<NegotiationRequest> OutgoingRequests { get; private set; } = [];
        public List<NegotiationRequest> IncomingRequests { get; private set; } = [];
        public IdValueObject RoomId { get; private set; }
        public Room Room { get; private set; }
        public IdValueObject? GameId { get; private set; }
        public DomainGame Game { get; private set; }
        public Order Order { get; private set; }

        //EF
        private Country() {}

        private Country(string countryName, string normalizedName, string flagImagePath, Guid roomId)
        {
            Id = Guid.NewGuid();
            CountryName = countryName;
            NormalizedName = normalizedName;
            FlagImagePath = flagImagePath;
            LivingLevel = LivingLevel.Create();
            Budget = Budget.Create();
            HaveNuclearTechnology = false;
            NuclearTechnology = NuclearTechnology.Create();
            Income = Income.Create();
            HasAppliedOrder = false;
            HasValidatedOrder = false;
            RoomId = roomId;
        }

        private Country(string countryName, string normalizedName, string flagImagePath, Guid roomId, ICountryStrategy strategy)
        {
            Id = Guid.NewGuid();
            CountryName = countryName;
            NormalizedName = normalizedName;
            FlagImagePath = flagImagePath;
            LivingLevel = LivingLevel.Create();
            Budget = Budget.Create();
            HaveNuclearTechnology = false;
            NuclearTechnology = NuclearTechnology.Create();
            Income = Income.Create();
            HasAppliedOrder = false;
            HasValidatedOrder = false;
            RoomId = roomId;
            _strategy = strategy;
        }

        public static Country Create(string countryName, string normalizedName, string flagImagePath, Guid roomId, ICountryStrategy strategy)
        {
            return new Country(countryName, normalizedName, flagImagePath, roomId, strategy);
        }

        public void InitializeStrategy(IServiceProvider provider)
        {
            _strategy = provider.GetRequiredService<CountryStrategyFactory>().CreateStrategy(NormalizedName);
        }

        public void AddCity(City city)
        {
            if (Cities.Any(c => (c.Id == city.Id || c.NormalizedName == city.NormalizedName)))
                throw new BusinessRuleValidationException("Cities in Country must be unique");
            Cities.Add(city);
        }

        public void AddPlayer(RoomMember member, bool hasTeams)
        {
            if (Players.Count == 0)
                member.PromoteToRole(GameRole.President);

            if (Players.Any(p => p.GameRole == GameRole.President) && member.GameRole == GameRole.President)
                member.PromoteToRole(GameRole.Minister);

            if (Players.Any(m => m.GameUserId == member.GameUserId))
                throw new BusinessRuleValidationException("Cannot add same Player in Country");

            if (Players.Count == 0 && member.GameRole != GameRole.President)
                throw new BusinessRuleValidationException("First Member in Player must be President");

            if (!hasTeams && Players.Count == 1)
                throw new BusinessRuleValidationException("Cannot add second Player in Country when Room created without teams");

            if (Players.Where(p => (p.GameRole == GameRole.President)).Count() == 1 && (member.GameRole == GameRole.President))
                throw new BusinessRuleValidationException("Country can have only one President");

            Players.Add(member);
        }

        public void RemovePlayer(RoomMember member)
        {
            if (!Players.Any(p => p.GameUserId == member.GameUserId))
                throw new BusinessRuleValidationException("To remove a Player from a Country, they must belong to that Country");

            if (Players.Count < 1)
                throw new BusinessRuleValidationException("Country must have at least 1 Player to remove");

            Players.Remove(member);
        }

        public RoomMember ElectNewPresident()
        {
            if (Players.Any(p => p.GameRole == GameRole.President))
                throw new BusinessRuleValidationException("Cannot elect a new President in a Country with President");

            var presidentCandidate = Players.FirstOrDefault(p => p.GameRole == GameRole.Minister)
                ?? throw new BusinessRuleValidationException("Country must have at least 1 Minister to promote him to President");

            presidentCandidate.PromoteToRole(GameRole.President);

            return presidentCandidate;
        }

        public List<int> GetAllPrices()
        {
            return
            [
                _strategy.NuclearTechnologyCost,
                _strategy.BombCost,
                _strategy.ShieldCost,
                _strategy.CityDevelopmentCost
            ];
        }

        public void AcceptDonation(int donationValue)
        {
            Budget += donationValue;
        }

        public void UpdateState(int ecologyLevel)
        {
            // Updating Budget
            Budget += Income;

            // Updating Income
            Income = 0;
            foreach(var city in Cities)
            {
                Income += _strategy.CalculateCityIncome(this, city, ecologyLevel, IncomingSanctions);
            }

            // Updating flags
            HasAppliedOrder = false;
            HasValidatedOrder = false;
        }

        public void ValidateOrder(RoomMember member, Order order, DomainGame currentGame)
        {
            var countries = currentGame.Countries;

            //Common
            if (currentGame.GameState != GameState.OrderMaking)
                throw new BusinessRuleValidationException("Cannot validate orders not in OrderMaking game state");

            if (HasAppliedOrder)
                throw new BusinessRuleValidationException("Cannot validate orders with already applied order");

            if (HasValidatedOrder)
                throw new BusinessRuleValidationException("Cannot validate orders with already validated order");

            if (member.CountryId != Id)
                throw new BusinessRuleValidationException("Member must belong to Country to apply Order");

            if (member.GameRole != GameRole.President)
                throw new BusinessRuleValidationException("Only President can apply Orders");

            if (HasAppliedOrder)
                throw new BusinessRuleValidationException("Cannot apply Order when Order already applied for this round");

            if (order.CalculateTotalCost(_strategy) > Budget)
                throw new BusinessRuleValidationException("Order's total cost must be less or equal to Country's budget to apply Order");

            //Develop City
            if (order.CitiesToDevelop.Except(Cities.Select(c => c.Id)).Any())
                throw new BusinessRuleValidationException("Can develop only Country's Cities");

            if(order.CitiesToDevelop.Any(oc => !Cities.First(c=> c.Id == oc).IsAlive))
                throw new BusinessRuleValidationException("Cannot develop destroyed City");

            //Set shield
            if (order.CitiesToSetShield.Except(Cities.Select(c => c.Id)).Any())
                throw new BusinessRuleValidationException("Can set shield for only Country's Cities");

            if (order.CitiesToSetShield.Any(oc => Cities.First(c => c.Id == oc).HaveShield))
                throw new BusinessRuleValidationException("Cannot set a shield to a City with shield");

            if(order.CitiesToSetShield.Any(oc => !Cities.First(c => c.Id == oc).IsAlive))
                throw new BusinessRuleValidationException("Can set shield only for alive Cities");

            //Ecology Program
            if (order.DevelopEcologyProgram && currentGame.EcologyLevel.IsGood())
                throw new BusinessRuleValidationException("Cannot develop ecology when it is in good state");

            //NT
            if (order.DevelopNuclearTechology && HaveNuclearTechnology)
                throw new BusinessRuleValidationException("Cannot develop nuclear technology when Country already has it");

            //Buy bombs
            if (order.BombsToBuyQuantity > 0 && !HaveNuclearTechnology)
                throw new BusinessRuleValidationException("Cannot buy bombs without nuclear technology");

            //Strike city
            if (order.CitiesToStrike.Count > 0 && !HaveNuclearTechnology)
                throw new BusinessRuleValidationException("Cannot strike Cities without nuclear technology");

            if (order.CitiesToStrike.Intersect(Cities.Select(c => c.Id)).Any() ||
                order.CitiesToStrike.Except(countries.SelectMany(c => c.Cities).Select(c => c.Id)).Any())
                throw new BusinessRuleValidationException("Can strike only Cities of other Countries");

            if (order.CitiesToStrike.Count > NuclearTechnology)
                throw new BusinessRuleValidationException("Strike quantity must be less or equal to Country's bomb quantity");

            if (order.CitiesToStrike.Any(oc => !countries.SelectMany(c => c.Cities).First(c => c.Id == oc).IsAlive))
                throw new BusinessRuleValidationException("Can strike only alive cities");

            //Sanctions
            if (order.CountriesToSetSanctions.Count > _strategy.SanctionQuantityInRoundLimit)
                throw new BusinessRuleValidationException($"Cannot exceed Sanctions limit");

            if (order.CountriesToSetSanctions.Except(countries.Select(c => c.Id)).Any() || order.CountriesToSetSanctions.Contains(Id))
                throw new BusinessRuleValidationException("Can send sanctions for only other Countries in Room");

            // Donations
            if (order.CountriesToDonate.ContainsKey(Id))
                throw new BusinessRuleValidationException("Cannot donate to itself");

            if (order.CountriesToDonate.Keys.Except(countries.Select(c => c.Id)).Any())
                throw new BusinessRuleValidationException("Can only donate to countries in room");

            HasValidatedOrder = true;
        }

        public void ApplyOrder(Order order, List<Country> countries, DomainGame currentGame)
        {
            if(!HasValidatedOrder)
                throw new BusinessRuleValidationException("Can apply only validated order");

            Budget -= order.CalculateTotalCost(_strategy);

            foreach (var city in Cities)
            {
                //Develop city
                if (order.CitiesToDevelop.Contains(city.Id))
                    city.DevelopCity();

                //Set shield
                if (order.CitiesToSetShield.Contains(city.Id))
                    city.SetShield();
            }

            //Ecology Program
            if(order.DevelopEcologyProgram)
                currentGame.DevelopEcologyProgram();

            //NT
            if (order.DevelopNuclearTechology)
                HaveNuclearTechnology = true;

            //Buy bombs
            NuclearTechnology += order.BombsToBuyQuantity;

            //Strike city
            foreach(var city in countries.SelectMany(c => c.Cities))
            {
                if (order.CitiesToStrike.Contains(city.Id))
                    city.GetStrike();
            }

            //Sanctions
            OutgoingSanctions = [];
            foreach (var countryId in order.CountriesToSetSanctions)
            {
                OutgoingSanctions.Add(Sanction.Create(Id, countryId, _strategy.SanctionPower));
            }

            foreach (var country in countries)
            {
                if (order.CountriesToDonate.TryGetValue(country.Id, out int value))
                {
                    country.AcceptDonation(value);
                }
            }

            HasAppliedOrder = true;
        }
    }
}