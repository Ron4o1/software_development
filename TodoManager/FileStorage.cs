using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TodoManager
{
    internal class FileStorage
    {
        private readonly string path;

        public FileStorage(string path)
        {
            this.path = path;
        }

        public TodoStorage Load()
        {
            if(!File.Exists(path))
            {
                return new TodoStorage();
            }

            string json = File.ReadAllText(path);
            TodoStorage todoStorage = JsonSerializer.Deserialize<TodoStorage>(json);
            return todoStorage;
        }

        public void Save(TodoStorage todoStorage)
        {
            string json = JsonSerializer.Serialize(todoStorage);
            File.WriteAllText(path, json);
        }
    }
}
