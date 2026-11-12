using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TodoManager
{
    internal class TodoStorage
    {
        public int NextId { get; set; } = 1;
        public List<TodoItem> Todos { get; set; } = new();
    }
}
