using System.Globalization;

namespace ClubManager
{
    internal class Program
    {
        private static ClubService _clubService = null!;

        public static void Main(string[] args)
        {
            ClubRepository repository = new ClubRepository();
            _clubService = new ClubService(repository);

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n===== CLUB MANAGER =====");
                Console.WriteLine("1. Покажи всички клубове");
                Console.WriteLine("2. Добави клуб");
                Console.WriteLine("3. Изтрий клуб");
                Console.WriteLine("4. Покажи клубовете за избрана дата");
                Console.WriteLine("0. Изход");
                Console.Write("Избери действие: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        Add();
                        break;
                    case "3":
                        Delete();
                        break;
                    case "4":
                        FilterByDate();
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("Програмата приключи.");
                        break;
                    default:
                        Console.WriteLine("Невалиден избор. Опитай отново.");
                        break;
                }
            }
        }

        // Показва всички клубове.
        private static void ShowAll()
        {
            var clubs = _clubService.GetAllClubs();

            if (clubs.Count == 0)
            {
                Console.WriteLine("Няма записани клубове.");
                return;
            }

            Console.WriteLine("\nВсички клубове:");
            foreach (var club in clubs)
            {
                Console.WriteLine($"Id: {club.Id} | Име: {club.Name} | Дата: {club.Date:dd.MM.yyyy}");
            }
        }

        // Въвежда данните и добавя нов клуб.
        private static void Add()
        {
            Console.Write("Въведи име на клуба: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Въведи дата (дд.мм.гггг): ");
            string dateInput = Console.ReadLine() ?? string.Empty;

            if (!DateTime.TryParse(dateInput, CultureInfo.GetCultureInfo("bg-BG"), DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Грешка: Невалиден формат на датата.");
                return;
            }

            try
            {
                _clubService.AddClub(name, date);
                Console.WriteLine("Клубът е добавен успешно.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
        }

        // Показва клубовете и изтрива избрания по Id.
        private static void Delete()
        {
            ShowAll();

            Console.Write("Въведи Id на клуба за изтриване: ");
            string idInput = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Грешка: Id трябва да бъде цяло число.");
                return;
            }

            try
            {
                _clubService.DeleteClub(id);
                Console.WriteLine("Клубът е изтрит успешно.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
        }

        // Показва клубовете, които са за избраната дата.
        private static void FilterByDate()
        {
            Console.Write("Въведи дата (дд.мм.гггг): ");
            string dateInput = Console.ReadLine() ?? string.Empty;

            if (!DateTime.TryParse(dateInput, CultureInfo.GetCultureInfo("bg-BG"), DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Грешка: Невалиден формат на датата.");
                return;
            }

            var clubs = _clubService.GetClubsByDate(date);

            if (clubs.Count == 0)
            {
                Console.WriteLine("За тази дата няма клубове.");
                return;
            }

            Console.WriteLine($"Клубове за {date:dd.MM.yyyy}:");
            foreach (var club in clubs)
            {
                Console.WriteLine($"Id: {club.Id} | Име: {club.Name} | Дата: {club.Date:dd.MM.yyyy}");
            }
        }
    }
}
