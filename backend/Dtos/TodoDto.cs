using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using backend.Dtos;

namespace backend.Dtos
{
    public class TodoDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime? CompleteAt { get; set; }
    }
}

