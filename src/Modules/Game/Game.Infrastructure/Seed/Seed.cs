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
            var usa = new CountryPattern("США", "UNITED_STATES", "");
            var northKorea = new CountryPattern("Северная Корея", "NORTH_KOREA", "");
            var iran = new CountryPattern("Иран", "IRAN", "");
            var cuba = new CountryPattern("Куба", "CUBA", "");
            var switzerland = new CountryPattern("Швейцария", "SWITZERLAND", "");
            var greatBritain = new CountryPattern("Великобритания", "GREAT_BRITAIN", "");

            Countries.AddRange(new[] { russia, china, japan, germany, france, usa, northKorea, iran, cuba, switzerland, greatBritain });

            var russiaCities = new List<CityPattern>
            {
                new CityPattern("Москва", "MOSCOW", "", russia.Id, true),
                new CityPattern("Санкт-Петербург", "SAINT_PETERSBURG", "", russia.Id),
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
                new CityPattern("Вашингтон", "WASHINGTON", "", usa.Id, true),
                new CityPattern("Нью-Йорк", "NEW_YORK_CITY", "", usa.Id),
                new CityPattern("Лос-Анджелес", "LOS_ANGELES", "", usa.Id),
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
               new CityPattern("Санктьяго-де-Куба","SANTIAGO_DE_CUBA" , "" , cuba.Id),
               new CityPattern("Камагуэй","CAMAGUEY" , "" , cuba.Id),
               new CityPattern("Ольгин","OLGUIN" , "" , cuba.Id)
           };

            Cities.AddRange(cubaCities);

            var switzerlandCities = new List<CityPattern>
           {
               new CityPattern("Берн","BERNE" , "" , switzerland.Id, true),
               new CityPattern("Цюрих","ZURICH" , "" , switzerland.Id),
               new CityPattern("Женева","GENEVA" , "" , switzerland.Id),
               new CityPattern("Базель","BASEL" , "" , switzerland.Id)
           };

            Cities.AddRange(switzerlandCities);

            var greatBritainCities = new List<CityPattern>
           {
               new CityPattern("Лондон","LONDON" , "" , greatBritain.Id, true),
               new CityPattern("Бирмингем","BIRMINGHAM" , "" , greatBritain.Id),
               new CityPattern("Глазго","GLASGOW" , "" , greatBritain.Id),
               new CityPattern("Манчестер","MANCHESTER" , "" , greatBritain.Id)
           };

            Cities.AddRange(greatBritainCities);
        }
    }
}
