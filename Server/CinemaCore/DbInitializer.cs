using WebApplicationLab6;
using CinemaCore.Models;

namespace CinemaCore
{
    public static class DbInitializer
    {
        public static void Initialize(CinemaContext context)
        {
            context.Database.EnsureCreated();

            string[] сountries = { "Армения", "Казахстан", "Беларусь", "Россия", "Турция", "США", "Франция", "Финляндия", "Бразилия", "Индия", "Конго", "ЮАР", "Северная Корея", "Китай", "Канада", "Германия", "Египет", "Польша", "Украина", "Литва", "Латвия", "Нидерланды", "Дания", "Сирия", "Монголия", "Мексика", "Англия", "Португалия", "Италия", "Австрия", "Япония" };

            //Жанры фильмов
            string[] genres = { "Комедия", "Ужасы", "Боевик", "Приключение", "Триллер", "Мистика", "Детектив", "Биография", "Драма", "Мелодрама", "Фантастика", "Исторический", "Военный", "Семейный", "Вестерн", "Трагедия", "Фэнтэзи" };

            Random random = new Random();

            if (context.CountryProductions.Count() == 0)
            {
                for (int i = 0; i < 500; i++)
                {
                    var value = сountries[random.Next(0, сountries.Length - 1)];
                    context.CountryProductions.Add(new CountryProductions { Name = value });
                }
                context.SaveChanges();
            }

            if (context.Genres.Count() == 0)
            {
                for (int i = 0; i < 500; i++)
                {
                    var value = genres[random.Next(0, genres.Length - 1)];
                    context.Genres.Add(new Genres { Name = value });
                }
                context.SaveChanges();
            }

            if (context.FilmProductions.Count() == 0)
            {
                for (int i = 0; i < 500; i++)
                {
                    var value = $"Company {i}";
                    context.FilmProductions.Add(new FilmProductions { Name = value });
                }
                context.SaveChanges();
            }

            if (context.Films.Count() == 0)
            {
                for (int i = 0; i < 500; i++)
                {
                    string filmName = "Название_" + i.ToString();
                    int genreId = random.Next(1, 500);
                    int duration = random.Next(5, 230);
                    int filmProductionId = random.Next(1, 500);
                    int countryProductionId = random.Next(1, 500);
                    int ageLimit = random.Next(0, 18);
                    string description = "Описание_" + i.ToString();
                    context.Films.Add(new Films
                    {
                        Name = filmName,
                        GenreId = genreId,
                        Duration = duration,
                        FilmProductionId = filmProductionId,
                        CountryProductionId = countryProductionId,
                        AgeLimit = ageLimit,
                        Description = description
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
