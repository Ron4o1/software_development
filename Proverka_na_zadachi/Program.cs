using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Proverka_na_zadachi
{
    internal class Program
    {
        private const string FilePath = "zadachi.txt";
        static void Main(string[] args)
        {
            Console.WriteLine(File.Exists(FilePath));
            Console.WriteLine(Path.GetFullPath(FilePath));
            List<Zadachi> zadachi = LoadZadachi(FilePath);
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНИДЖЪР НА ЗАДАЧИ ===");
                Console.WriteLine("1. Добавяне на нова задача"); 
                Console.WriteLine("2. Преглед на всички задачи"); 
                Console.WriteLine("3. Маркиране на задача като изпълнена"); 
                Console.WriteLine("4. Изтриване на задача");
                Console.WriteLine("5. Изход"); 
                Console.Write("\nИзберете опция (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Въведете Заглавие: ");
                        string productID = Console.ReadLine();

                        Console.Write("Въведете име: ");
                        string title = Console.ReadLine();

                        Console.Write("Въведете описание: ");
                        string description = Console.ReadLine();

                        Console.Write("Въведете крайна дата: ");
                        string deadline = Console.ReadLine();

                        Console.Write("Задачата готова ли е: ");
                        bool IsComplated = bool.Parse(Console.ReadLine());

                        Zadachi newZadachi = new Zadachi(title, description, deadline, IsComplated);
                        zadachi.Add(newZadachi);

                        SaveProductsToFile(zadachi);
                        Console.WriteLine("Успешно добавен нов запис!");
                        Console.WriteLine();
                        break;

                    case "2":
                        ShowTasks(zadachi);
                        WaitForKey(zadachi);
                        break;

                    case "3":
                        MarkTaskCompleted(zadachi);
                        break;

                    case "4":
                        DeleteTask(zadachi);
                        break;

                    case "5":
                        running = false; 
                        Console.WriteLine("Довиждане!");
                        break;

                    default:
                        Console.WriteLine("Невалиден избор. Натиснете бутон за продължение...");
                        WaitForKey(zadachi);
                        break;
                }
            }
        }
        static void SaveProductsToFile(List<Zadachi> products)
        {
            List<string> rows = new List<string>();
            foreach (Zadachi z in products)
            {
                rows.Add(z.ToFileRow());
            }
            File.WriteAllLines(FilePath, rows);
        }


        static List<Zadachi> LoadZadachi(string FilePath)
        {
            List<Zadachi> zadachi = new List<Zadachi>();

            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
                    Zadachi z = Zadachi.FromFileRow(line);
                    if (z != null)
                    {
                        zadachi.Add(z);
                    }
                }
            }
            return zadachi;
        }
        static void AddTask(List<Zadachi> zadachi, bool isCompleted)
        {
            Console.Clear();
            Console.WriteLine("--- ДОБАВЯНЕ НА НОВА ЗАДАЧА ---");

            Console.Write("Въведете заглавие: "); 
            string title = Console.ReadLine();

            Console.Write("Въведете описание: "); 
            string description = Console.ReadLine();

            Console.Write("Въведете краен срок: "); 
            string deadline = Console.ReadLine();

            zadachi.Add(new Zadachi(title, description, deadline, IsCompleted);
            Console.WriteLine("\nЗадачата беше добавена успешно!");
            WaitForKey(zadachi);
            Zadachi z = new Zadachi(title, description, deadline, IsCompleted);

            zadachi.Add(z);

            SaveProductsToFile(zadachi);

            Console.WriteLine("Продуктът е добавен успешно!");
        }

        static void ShowTasks(List<Zadachi> zadachi)
        {
            Console.Clear();
            Console.WriteLine("--- СПИСЪК СЪС ЗАДАЧИ ---");

            if (zadachi.Count == 0)
            {
                Console.WriteLine("Няма намерени задачи.");
                return;
            }

            for (int i = 0; i < zadachi.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {zadachi[i]}");
                Console.WriteLine(new string('-', 40));
            }
        }
        static void MarkTaskCompleted(List<Zadachi> zadachi)
        {
            ShowTasks(zadachi);
            if (zadachi.Count == 0)
            {
                WaitForKey(zadachi);
                return;
            }

            Console.Write("\nВъведете номера на задачата, която искате да маркирате като изпълнена: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= zadachi.Count)
            {
                zadachi[index - 1].IsCompleted = true; 
                Console.WriteLine("Задачата беше маркирана като изпълнена!");
            }
            else
            {
                Console.WriteLine("Невалиден номер на задача.");
            }
            WaitForKey(zadachi);
        }

        static void DeleteTask(List<Zadachi> zadachi)
        {
            ShowTasks(zadachi);
            if (zadachi.Count == 0)
            {
                WaitForKey(zadachi);
                return;
            }

            Console.Write("\nВъведете номера на задачата за изтриване: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= zadachi.Count)
            {
                zadachi.RemoveAt(index - 1);
                Console.WriteLine("Задачата беше изтрита успешно!");
            }
            else
            {
                Console.WriteLine("Невалиден номер на задача.");
            }
            WaitForKey(zadachi);
        }

        static void WaitForKey(List<Zadachi> zadachi)
        {
            Console.WriteLine("\nНатиснете произволен клавиш, за да се върнете към менюто...");
            Console.ReadKey();
        }
    }
}