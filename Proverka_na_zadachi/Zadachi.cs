using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proverka_na_zadachi
{
    internal class Zadachi
    {
        public Zadachi(string title, string description, string deadline, bool isComplete)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
            IsCompleted = false;
        }

        public Zadachi(string? title, string? description, string? deadline)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Deadline { get; set; }

        public bool IsCompleted { get; set; }


        public string ToFileRow()
        {
            return $"{Title}; {Description}; {Deadline}; {IsCompleted}; ";
        }

        public override string ToString()
        {
            return $"Заглавие: {Title}, Описание: {Description}, Краен срок: {Deadline}, Готова ли е: {IsCompleted}";
        }

        public static Zadachi FromFileRow(string row)
        {
            string[] parts = row.Split(';');
            if (parts.Length == 5)
            {
                string title = parts[0];
                string description = parts[1];
                string deadline = parts[2];
                bool IsComplete  = bool.Parse(parts[3]);
                return new Zadachi(title, description, deadline, IsComplete);
            }
            return null;
        }
    }
}
