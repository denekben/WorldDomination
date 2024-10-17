using User.Domain.Entities;

namespace User.Infrastructure.Configurations
{
    public static class Seed
    {
        public static List<Achievment> Achievments { get; private set; } = [];
        static Seed()
        {
            Achievments =
            [
                Achievment.Create(
                    "Великий вождь",
                    "Выиграйте игру в роли президента"
                ),
                Achievment.Create(
                    "Давай давай нападай",
                    "Произведите 5 ядерных бомб"
                ),
                Achievment.Create(
                    "Сильный и независимый",
                    "Выиграйте игру, будучи обложенным санкциями всех стран"
                ),
                Achievment.Create(
                    "Радиоактивный пепел",
                    "Сбросьте ядерную бомбу"
                )
            ];
        }
    }
}
