using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TodoManager
{
    internal class TodoService
    {
        //private readonly static List<TodoItem> todos = new List<TodoItem>();
        //private static int nextId = 1;

        private readonly TodoStorage storage;
        private readonly FileStorage fileStorage;
        public TodoService(FileStorage file)
        {
            fileStorage = file;
            this.storage = fileStorage.Load();
        }
        public IReadOnlyList<TodoItem> GetAll()
        {
            return storage.Todos;
        }
        public TodoItem FindById(int id)
        {
            foreach (TodoItem todoItem in storage.Todos)
            {
                if(todoItem.Id == id)
                {
                    return todoItem;
                }

            }
            return null;

        }

        public void Add(string title, string description, DateTime dueDate)
        {
            TodoItem todoItem = new TodoItem(storage.NextId, title, description, dueDate);
            storage.Todos.Add(todoItem);
            storage.NextId++;

            fileStorage.Save(storage);
        }

        public void Complate(int id)
        {
            TodoItem complatedTask = FindById(id);

            if (complatedTask == null)
                throw new ArgumentException("Няма задача с токава ID.");

            complatedTask.Completed();

            fileStorage.Save(storage);
        }
        public void Delete(int id)
        {
            TodoItem deleteTask = FindById(id);

            if (deleteTask == null)
                throw new ArgumentException("Няма задача с токава ID.");

            storage.Todos.Remove(deleteTask);

            fileStorage.Save(storage);
        }
    }
}
