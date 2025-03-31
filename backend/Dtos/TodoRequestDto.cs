using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace TodoController.Dtos
{
    public class TodoRequestDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime? CompleteAt { get; set; }
    }

    public class TodoValidator : AbstractValidator<TodoRequestDto> {
        public TodoValidator(){
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title of task is required")
            .MinimumLength(5).WithMessage("Task must not be shorter than 5 characters.")
            .MaximumLength(60).WithMessage("Task must not be longer than 60 characters.");
        }
    }
}