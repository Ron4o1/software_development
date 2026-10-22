using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoManager
{
    internal class Zadacha
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public bool IsCompleted { get; set; } 

        public Zadacha(int id,string title, string description, DateTime date, bool isCompleted)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("Id трябва да е положително");
            }
            if(string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("заглавие е задължително");
            }

            Id = id;
            Title = title;
            Description = description;
            Date = date;
            IsCompleted = isCompleted;
        }
    }
}
