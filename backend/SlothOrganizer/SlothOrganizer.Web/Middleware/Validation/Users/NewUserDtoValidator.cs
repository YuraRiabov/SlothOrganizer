﻿using System.Text.RegularExpressions;
using FluentValidation;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Contracts.Validation;

namespace SlothOrganizer.Web.Middleware.Validation.Users
{
    public class NewUserDtoValidator : AbstractValidator<NewUserDto>
    {
        public NewUserDtoValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);
            RuleFor(u => u.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                .Must(p => p.IsValidPassword());
        }
    }
}
