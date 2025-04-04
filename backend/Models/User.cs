using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User
    {
        public int Id { get; set;}
        public string? Username { get; set;}

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<TodoItem> TodoItems { get; set; } = new();
    }
}