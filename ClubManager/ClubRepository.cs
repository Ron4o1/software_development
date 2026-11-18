using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ClubManager
{
    internal class ClubRepository
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { WriteIndented = true };

        private ClubData _data;

        public ClubRepository()
        {
            // Файлът clubs.json се намира в работната папка на проекта.
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "clubs.json");
            _data = Load();

            // Ако NextId е по-малко или равно на вече използвано Id,
            // го поправяме, за да не се получават повтарящи се Id-та.
            int maxId = _data.Clubs.Count == 0 ? 0 : _data.Clubs.Max(club => club.Id);
            if (_data.NextId <= maxId)
            {
                _data.NextId = maxId + 1;
                Save();
            }
        }

        public List<Club> GetAll()
        {
            return _data.Clubs.ToList();
        }

        // Id се задава тук, а не от потребителя.
        public void Add(Club club)
        {
            club.Id = _data.NextId;
            _data.NextId++;

            _data.Clubs.Add(club);
            Save();
        }

        public bool Delete(int id)
        {
            Club? club = _data.Clubs.FirstOrDefault(c => c.Id == id);

            if (club == null)
            {
                return false;
            }

            _data.Clubs.Remove(club);
            Save();

            // NextId не се намалява при изтриване.
            return true;
        }

        private ClubData Load()
        {
            if (!File.Exists(_filePath))
            {
                return new ClubData();
            }

            string json = File.ReadAllText(_filePath);
            ClubData? loadedData = JsonSerializer.Deserialize<ClubData>(json);

            return loadedData ?? new ClubData();
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(_data, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
    }
}
