using Microsoft.VisualBasic.FileIO;
using System.Threading.Tasks;

namespace TodoManager
{
    internal class Program
    {
        //static TodoService service = new TodoService();
        static void Main(string[] args)
        {
            TodoStorage storage = new TodoStorage();
            TodoService service = new TodoService(storage);
            while (true)
            {
                Menu();
                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": { AddTodo(service); break; }
                        case "2": { ListAllTodos(service); break; }
                        case "3": { CompleteTodo(service); break; }
                        case "4": { DeleteTodo(service); break; }
                        case "5": { Console.WriteLine("Край на програмата"); Console.ReadLine(); return; }
                        default: { Console.WriteLine("Невалиден избор."); break; }
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("======= TODO MANAGER ========");
            Console.WriteLine("1. Добави задача");
            Console.WriteLine("2. Покажи задачи");
            Console.WriteLine("3. Маркирай задача като изпълнена");
            Console.WriteLine("4. Изтрий задача");
            Console.WriteLine("5. Изход");

            Console.WriteLine();
            Console.Write("Избор: ");
        }

        private static void DeleteTodo(TodoService service)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====  Изтрий задача =======\n");
            ListAllTodos(service);

            Console.Write("Избери Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Невалидно id.");
                Console.Write("Избери Id: ");
            }
            service.Delete(id);
            Console.WriteLine("Задачата е изтрита.");

        }

        private static void CompleteTodo(TodoService service)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====  Маркирай задача като завършена =======\n");
            ListAllTodos(service);

            Console.Write("Избери Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Невалидно id.");
                Console.Write("Избери Id: ");
            }

            service.Complate(id);
            Console.WriteLine("Задачата е завършена.");
        }

        private static void ListAllTodos(TodoService service)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("===== Всички задачи  =======");

            var todos = service.GetAll();

            if (todos.Count == 0)
            {
                Console.WriteLine("Няма въведени задачи");
            }
            else
            {
                foreach (TodoItem todoItem in todos)
                {
                    Console.WriteLine($"{todoItem}");
                }
            }
        }

        private static void AddTodo(TodoService service)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====  Добавяне на задача =======");

            Console.Write("Заглавие:");
            string title = Console.ReadLine();

            Console.Write("Описание:");
            string description = Console.ReadLine();

            Console.Write("Краен срок:");
            DateTime dueDate;
            while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.WriteLine("Невалидна дата.Въведи отово!");
                Console.Write("Краен срок: ");
            }

            service.Add(title, description,dueDate);
            Console.WriteLine("Задачата е добавена.");

        }
    }
}
