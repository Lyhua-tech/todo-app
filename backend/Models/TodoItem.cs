using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoController.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime? CompleteAt { get; set; }
    }
}