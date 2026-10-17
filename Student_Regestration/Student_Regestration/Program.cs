using System;
using System.Globalization;
using System.IO;

namespace Student_Regestration
{
    internal class Program
    {
        static int nextId = 1;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== РЕГИСТРАЦИЯ НА УЧЕНИК ===");
            Console.WriteLine();

            Student student = null;
            while (student == null)
            {
                Console.Write("Име: ");
                string name = Console.ReadLine() ?? "";

                try
                {
                    student = new Student(nextId, name);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Възраст: ");

                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine("Моля, въведете цяло число.");
                    continue;
                }

                try
                {
                    student.Age = age;
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Клас: ");

                if (!int.TryParse(Console.ReadLine(), out int grade))
                {
                    Console.WriteLine("Моля, въведете цяло число.");
                    continue;
                }

                try
                {
                    student.Grade = grade;
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Среден успех (например 5.65): ");

                if (!double.TryParse(
                        Console.ReadLine(),
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double score))
                {
                    Console.WriteLine("Невалиден формат на успеха.");
                    continue;
                }

                try
                {
                    student.AverageScore = score;
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Такса (например 120.50): ");

                if (!decimal.TryParse(
                        Console.ReadLine(),
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal fee))
                {
                    Console.WriteLine("Невалиден формат на таксата.");
                    continue;
                }

                try
                {
                    student.Fee = fee;
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Паралелка: ");

                if (!char.TryParse(
                        Console.ReadLine(),
                        out char letter))
                {
                    Console.WriteLine("Въведете само една буква.");
                    continue;
                }

                try
                {
                    student.ClassLetter = letter;
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Дата на раждане (дд.ММ.гггг): ");

                if (!DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date))
                {
                    Console.WriteLine(
                        "Невалиден формат. Например: 14.05.2009");
                    continue;
                }

                try
                {
                    student.BirthDate = date;
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Грешка: " + ex.Message);
                }
            }

            nextId++;
            Console.WriteLine();
            Console.WriteLine("=== ДАННИ ЗА УЧЕНИК ===");
            Console.WriteLine();
            Console.WriteLine($"Id: {student.Id}");
            Console.WriteLine($"Име: {student.Name}");
            Console.WriteLine($"Възраст: {student.Age}");
            Console.WriteLine($"Клас: {student.Grade}");
            Console.WriteLine(
                $"Среден успех: {student.AverageScore.ToString("F2", CultureInfo.InvariantCulture)}");
            Console.WriteLine(
                $"Такса: {student.Fee.ToString("F2", CultureInfo.InvariantCulture)} лв.");
            Console.WriteLine(
                $"Стипендия: {(student.HasScholarship ? "Да" : "Не")}");
            Console.WriteLine($"Паралелка: {student.ClassLetter}");
            Console.WriteLine(
                $"Дата на раждане: {student.BirthDate:dd.MM.yyyy}");
            student.SaveToFile();
            Console.WriteLine("Ученикът е записан успешно!");
        }
    }
}
