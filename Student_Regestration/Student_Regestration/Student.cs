using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Student_Regestration
{
    internal class Student
    {
        private string name = "";
        private int age;
        private int grade;
        private double averageScore;
        private decimal fee;
        private char classLetter;
        private DateTime birthDate;

        public int Id { get; }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Името не може да бъде празно.");
                }

                name = value.Trim();
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 6 || value > 20)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Age),
                        "Възрастта трябва да бъде между 6 и 20 години.");
                }

                age = value;
            }
        }

        public int Grade
        {
            get { return grade; }
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Grade),
                        "Класът трябва да бъде между 1 и 12.");
                }

                grade = value;
            }
        }

        public double AverageScore
        {
            get { return averageScore; }
            set
            {
                if (value < 2.0 || value > 6.0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(AverageScore),
                        "Успехът трябва да бъде между 2.00 и 6.00.");
                }

                averageScore = value;
            }
        }

        public decimal Fee
        {
            get { return fee; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Fee),
                        "Таксата не може да бъде отрицателна.");
                }

                fee = value;
            }
        }

        public bool HasScholarship
        {
            get
            {
                return AverageScore >= 5.50;
            }

        }
        public char ClassLetter
        {
            get { return classLetter; }
            set
            {
                if (!char.IsLetter(value))
                {
                    throw new ArgumentException(
                        "Паралелката трябва да бъде буква.");
                }

                classLetter = value;
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value.Date > DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(BirthDate),
                        "Датата на раждане не може да бъде в бъдещето.");
                }

                birthDate = value;
            }
        }

        public Student(int id, string name)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(id),
                    "Идентификаторът трябва да бъде положително число.");
            }

            Id = id;
            Name = name;
        }
        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter("students.txt", true))
            {
                writer.WriteLine(
                    $"{Id};{Name};{Age};{Grade};" +
                    $"{AverageScore.ToString(System.Globalization.CultureInfo.InvariantCulture)};" +
                    $"{Fee.ToString(System.Globalization.CultureInfo.InvariantCulture)};" +
                    $"{HasScholarship};{ClassLetter};{BirthDate:dd.MM.yyyy}");
            }
        }
    }
}