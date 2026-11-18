using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManager
{
    internal class ClubService
    {
        private readonly ClubRepository _repository;

        public ClubService(ClubRepository repository)
        {
            _repository = repository;
        }

        public List<Club> GetAllClubs()
        {
            return _repository.GetAll();
        }

        // Проверява данните, преди да изпрати клуба към репозиторията.
        public void AddClub(string name, DateTime date)
        {
            name = (name ?? string.Empty).Trim();

            if (name.Length < 3)
            {
                throw new ArgumentException("Името трябва да съдържа поне 3 символа.");
            }

            if (date.Date < DateTime.Today)
            {
                throw new ArgumentException("Датата не може да бъде в миналото.");
            }

            Club club = new Club
            {
                Name = name,
                Date = date.Date
            };

            _repository.Add(club);
        }

        public void DeleteClub(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Невалидно Id.");
            }

            bool isDeleted = _repository.Delete(id);

            if (!isDeleted)
            {
                throw new ArgumentException("Не съществува клуб с такова Id.");
            }
        }

        public List<Club> GetClubsByDate(DateTime date)
        {
            return _repository.GetAll()
                .Where(club => club.Date.Date == date.Date)
                .ToList();
        }
    }
}
