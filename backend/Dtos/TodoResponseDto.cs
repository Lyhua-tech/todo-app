using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class TodoResponseDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        public string? Content { get; set; }

        public DateTime? CompleteAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}