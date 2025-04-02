using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace backend.Dtos
{
    public class RegisterDto
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set;}
    }

    public class RegisterValidator : AbstractValidator<RegisterDto> {
        public RegisterValidator(){
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be longer than 3 characters")
            .MaximumLength(50).WithMessage("Username must be shorter than 50 characters");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(3).WithMessage("Email must be longer than 3 characters")
            .MaximumLength(100).WithMessage("Email must be shorter than 100 characters");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(3).WithMessage("Password must be longer than 3 characters")
            .MaximumLength(20).WithMessage("Password must be shorter than 50 characters");
        }
    }

    public class LoginDto
    {
        public string? Username { get; set; }

        public string? Password { get; set;}
    }

    public class LoginValidator : AbstractValidator<LoginDto> {
        public LoginValidator(){
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
        }
    }

    public class AuthResponseDto {
        public int UserId { get; set; }
        public string? Username { get; set; }

        public string? Token { get; set; }
    }
}