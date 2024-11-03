namespace Game.Infrastructure.Seed
{
    public static class Seed
    {
        public static List<CountryPattern> Countries { get; private set; }  = [];
        public static List<CityPattern> Cities { get; private set; } = [];

        static Seed()
        {
            var russia = new CountryPattern("Россия", "RUSSIA", "");
            var china = new CountryPattern("Китай", "CHINA", "");
            var japan = new CountryPattern("Япония", "JAPAN", "");
            var germany = new CountryPattern("Германия", "GERMANY", "");
            var france = new CountryPattern("Франция", "FRANCE", "");
            var usa = new CountryPattern("США", "UNITED STATES", "");
            var northKorea = new CountryPattern("Северная Корея", "NORTH KOREA", "");
            var iran = new CountryPattern("Иран", "IRAN", "");
            var cuba = new CountryPattern("Куба", "CUBA", "");
            var australia = new CountryPattern("Австралия", "AUSTRALIA", "");
            var southAfrica = new CountryPattern("Южная Африка", "SOUTH AFRICA", "");

            Countries.AddRange(new[] { russia, china, japan, germany, france, usa, northKorea, iran, cuba, australia, southAfrica });

            var russiaCities = new List<CityPattern>
            {
                new CityPattern("Москва", "MOSCOW", "", russia.Id, true),
                new CityPattern("Санкт-Петербург", "SAINT PETERSBURG", "", russia.Id),
                new CityPattern("Екатеринбург", "YEKATERINBURG", "", russia.Id),
                new CityPattern("Новосибирск", "NOVOSIBIRSK", "", russia.Id)
            };

            Cities.AddRange(russiaCities);

            var chinaCities = new List<CityPattern>
            {
                new CityPattern("Пекин", "BEIJING", "", china.Id, true),
                new CityPattern("Шанхай", "SHANGHAI", "", china.Id),
                new CityPattern("Чунцин", "CHONGQING", "", china.Id),
                new CityPattern("Тяньцзинь", "TIANJIN", "", china.Id)
            };

            Cities.AddRange(chinaCities);

            var japanCities = new List<CityPattern>
            {
                new CityPattern("Токио", "TOKYO", "", japan.Id, true),
                new CityPattern("Йокогама", "YOKOHAMA", "", japan.Id),
                new CityPattern("Осака", "OSAKA", "", japan.Id),
                new CityPattern("Нагоя", "NAGOYA", "", japan.Id)
            };

            Cities.AddRange(japanCities);

            var germanyCities = new List<CityPattern>
            {
                new CityPattern("Берлин", "BERLIN", "", germany.Id),
                new CityPattern("Гамбург", "HAMBURG", "", germany.Id),
                new CityPattern("Мюнхен", "MUNICH", "", germany.Id),
                new CityPattern("Кёльн", "COLOGNE", "", germany.Id)
            };

            Cities.AddRange(germanyCities);

            var franceCities = new List<CityPattern>
            {
                new CityPattern("Париж", "PARIS", "", france.Id, true),
                new CityPattern("Марсель", "MARSEILLE", "", france.Id),
                new CityPattern("Лион", "LYON", "", france.Id),
                new CityPattern("Тулуза", "TOULOUSE", "", france.Id)
            };

            Cities.AddRange(franceCities);

            var usaCities = new List<CityPattern>
            {
                new CityPattern("Вашингтон", "WASHINGTON D.C.", "", usa.Id, true),
                new CityPattern("Нью-Йорк", "NEW YORK CITY", "", usa.Id),
                new CityPattern("Лос-Анджелес", "LOS ANGELES", "", usa.Id),
                new CityPattern("Чикаго", "CHICAGO", "", usa.Id)
            };

            Cities.AddRange(usaCities);

            var northKoreaCities = new List<CityPattern>
            {
                new CityPattern("Пхеньян", "PYONGYANG", "", northKorea.Id, true),
                new CityPattern("Хамхын","HAMHUNG","" , northKorea.Id),
                new CityPattern("Чхонджин","CHONJIN","" , northKorea.Id),
                new CityPattern("Нампхо","NAMPO" , "" , northKorea.Id)
           };

            Cities.AddRange(northKoreaCities);

            var iranCities = new List<CityPattern>
           {
               new CityPattern("Тегеран","TEHRAN" , "" , iran.Id, true),
               new CityPattern("Мешхед","MASHHAD" , "" , iran.Id),
               new CityPattern("Исфахан","ISFAHAN" , "" , iran.Id),
               new CityPattern("Кередж","KARAJ" , "" , iran.Id)
           };

            Cities.AddRange(iranCities);

            var cubaCities = new List<CityPattern>
           {
               new CityPattern("Гавана","HAVANA" , "" , cuba.Id, true),
               new CityPattern("Санктьяго-де-Куба","SANTIAGO DE CUBA" , "" , cuba.Id),
               new CityPattern("Камагуэй","CAMAGUEY" , "" , cuba.Id),
               new CityPattern("Ольгин","OLGUIN" , "" , cuba.Id)
           };

            Cities.AddRange(cubaCities);

            var australiaCities = new List<CityPattern>
           {
               new CityPattern("Канберра","CANBERRA" , "" , australia.Id, true),
               new CityPattern("Сидней","SYDNEY" , "" , australia.Id),
               new CityPattern("Мельбурн","MELBOURNE" , "" , australia.Id),
               new CityPattern("Брисбен","BRISBANE" , "" , australia.Id)
           };

            Cities.AddRange(australiaCities);

            var southAfricaCities = new List<CityPattern>
           {
               new CityPattern("Кейптаун","CAPE TOWN" , "" , southAfrica.Id, true),
               new CityPattern("Йоханнесбург","JOHANNESBURG" , "" , southAfrica.Id),
               new CityPattern("Дурбан","DURBAN" , "" , southAfrica.Id),
               new CityPattern("Претория","PRETORIA" , "" , southAfrica.Id)
           };

            Cities.AddRange(southAfricaCities);
        }
    }
}
