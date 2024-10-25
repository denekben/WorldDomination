namespace Game.Infrastructure.Seed
{
    public static class Seed
    {
        public static List<CountryPattern> Countries { get; private set; }  = [];
        public static List<CityPattern> Cities { get; private set; } = [];

        static Seed()
        {
            var russia = new CountryPattern("Россия", "");
            var china = new CountryPattern("Китай", "");
            var japan = new CountryPattern("Япония", "");
            var germany = new CountryPattern("Германия", "");
            var france = new CountryPattern("Франция", "");
            var usa = new CountryPattern("США", "");
            var northKorea = new CountryPattern("Северная Корея", "");
            var iran = new CountryPattern("Иран", "");
            var cuba = new CountryPattern("Куба", "");
            var australia = new CountryPattern("Австралия", "");
            var southAfrica = new CountryPattern("Южная Африка", "");

            Countries.AddRange([russia, china, japan, germany, france, usa, northKorea, iran, cuba, australia, southAfrica]);

            var russiaCities = new List<CityPattern>
            {
                new CityPattern("Москва", "", russia.Id),
                new CityPattern("Санкт-Петербург", "", russia.Id),
                new CityPattern("Екатеринбург", "", russia.Id),
                new CityPattern("Новосибирск", "", russia.Id)
            };

            Cities.AddRange(russiaCities);

            var chinaCities = new List<CityPattern>
            {
                new CityPattern("Пекин", "", china.Id),
                new CityPattern("Шанхай", "", china.Id),
                new CityPattern("Чунцин", "", china.Id),
                new CityPattern("Тяньцзинь", "", china.Id)
            };

            Cities.AddRange(chinaCities);

            var japanCities = new List<CityPattern>
            {
                new CityPattern("Токио", "", japan.Id),
                new CityPattern("Йокогама", "", japan.Id),
                new CityPattern("Осака", "", japan.Id),
                new CityPattern("Нагоя", "", japan.Id)
            };

            Cities.AddRange(japanCities);

            var germanyCities = new List<CityPattern>
            {
                new CityPattern("Берлин", "", germany.Id),
                new CityPattern("Гамбург", "", germany.Id),
                new CityPattern("Мюнхен", "", germany.Id),
                new CityPattern("Кёльн", "", germany.Id)
            };

            Cities.AddRange(germanyCities);

            var franceCities = new List<CityPattern>
            {
                new CityPattern("Париж", "", france.Id),
                new CityPattern("Марсель", "", france.Id),
                new CityPattern("Лион", "", france.Id),
                new CityPattern("Тулуза", "", france.Id)
            };

            Cities.AddRange(franceCities);

            var usaCities = new List<CityPattern>
            {
                new CityPattern("Вашингтон", "", usa.Id),
                new CityPattern("Нью-Йорк", "", usa.Id),
                new CityPattern("Лос-Анджелес", "", usa.Id),
                new CityPattern("Чикаго", "", usa.Id)
            };

            Cities.AddRange(usaCities);

            var northKoreaCities = new List<CityPattern>
            {
                new CityPattern("Пхеньян", "", northKorea.Id),
                new CityPattern("Хамхын", "", northKorea.Id),
                new CityPattern("Чхонджин", "", northKorea.Id),
                new CityPattern("Нампхо", "", northKorea.Id)
            };

            Cities.AddRange(northKoreaCities);

            var iranCities = new List<CityPattern>
            {
                new CityPattern("Тегеран", "", iran.Id),
                new CityPattern("Мешхед", "", iran.Id),
                new CityPattern("Исфахан", "", iran.Id),
                new CityPattern("Кередж", "", iran.Id)
            };

            Cities.AddRange(iranCities);

            var cubaCities = new List<CityPattern>
            {
                new CityPattern("Гавана", "", cuba.Id),
                new CityPattern("Санктьяго-де-Куба", "", cuba.Id),
                new CityPattern("Камагуэй", "", cuba.Id),
                new CityPattern("Ольгин", "", cuba.Id)
            };

            Cities.AddRange(cubaCities);

            var australiaCities = new List<CityPattern>
            {
                new CityPattern("Канберра", "", australia.Id),
                new CityPattern("Сидней", "", australia.Id),
                new CityPattern("Мельбурн", "", australia.Id),
                new CityPattern("Брисбен", "", australia.Id)
            };

            Cities.AddRange(australiaCities);

            var southAfricaCities = new List<CityPattern>
            {
                new CityPattern("Кейптаун", "", southAfrica.Id),
                new CityPattern("Йоханнесбург", "", southAfrica.Id),
                new CityPattern("Дурбан", "", southAfrica.Id),
                new CityPattern("Претория", "", southAfrica.Id)
            };

            Cities.AddRange(southAfricaCities);
        }
    }
}
