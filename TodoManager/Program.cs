using Microsoft.VisualBasic.FileIO;

namespace TodoManager
{
    internal class Program
    {
        static List<Zadacha> tasks = new List<Zadacha>();
        private static int nextId = 1;
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("=== ЗАДАЧИ ===");
                Console.WriteLine("1. Добавяне на задача");
                Console.WriteLine("2. Всички въведени задачи");
                Console.WriteLine("3. Избери задача, която да е изпълнена");
                Console.WriteLine("4. Изтрий задачата");
                Console.WriteLine("5. Изход");
                Console.WriteLine("Изберете опция (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddTitle();
                        break;

                    case "2":
                        ShowTasks();
                        break;

                    case "3":
                        CompleteTask();
                        break;
                        
                    case "4":
                        DeleteTask();
                        break;

                    case "5":
                        Console.WriteLine("Изход");
                        return;

                    default:
                        Console.WriteLine("Невалиден избор. Моля опитайте отново");
                        break;
                }

            }

        }
        
        static void AddTitle()
        {
            
            Console.WriteLine("--- Добавяне на нова задача ---");
            Console.Write("Въведете заглавие: ");
            string title = Console.ReadLine();
            Console.Write("Въведете описание: ");
            string description = Console.ReadLine();
            Console.Write("Въведете краен срок: ");
            DateTime date = DateTime.Parse(Console.ReadLine());
           
            Zadacha task = new Zadacha(nextId, title, description, date, false);
            tasks.Add(task);
            nextId++;
            Console.WriteLine("Задачата е добавена!");
        }

        static void ShowTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Няма въведени задачи");
                return;
            }
            else
            {
                foreach (Zadacha todoItem in tasks)
                {
                    string status = "";
                    if (todoItem.IsCompleted) status = "Завършена";
                    else status = "Незавършена";

                    Console.WriteLine($"{todoItem.Id}. {todoItem.Title}" +
                        $"\n {todoItem.Description}" +
                        $"\n {todoItem.Date:d}" +
                        $"\n Статус: {status}");
                }
            }
            
        }
        static void CompleteTask()
        {
            Console.Clear();
            Console.WriteLine("=====  Маркирай задача като завършена =======");
            foreach (Zadacha todoItem in tasks)
            {
                Console.WriteLine($"{todoItem.Id}. {todoItem.Title}");
            }
            Console.Write("Избери Id: ");
            int id = int.Parse(Console.ReadLine());

            Zadacha completedTask = null;

            foreach (Zadacha todoItem in tasks)
            {
                if (todoItem.Id == id)
                {
                    completedTask = todoItem;
                    break;
                }
            }
            if (completedTask != null)
            {
                completedTask.IsCompleted = true;
                Console.WriteLine("Задачата е изпълнена");
            }
            else
            {
                Console.WriteLine("Няма такава задача с ID");
            }
        }
        static void DeleteTask()
        {
            foreach (Zadacha todoItem in tasks)
            {
                Console.WriteLine($"{todoItem.Id}. {todoItem.Title}");
            }
            Console.Write("Избери Id: ");
            int id = int.Parse(Console.ReadLine());
            Zadacha deleteTask = null;

            foreach (Zadacha todoItem in tasks)
            {
                if (todoItem.Id == id)
                {
                    deleteTask = todoItem;
                    break;
                }
            }

            if (deleteTask != null)
            {
                tasks.Remove(deleteTask);
                Console.WriteLine("Задачата е изтрита.");
            }
            else
                Console.WriteLine("Няма задача с такова ID");


        }
    }
}
