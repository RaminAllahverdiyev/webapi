using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finder.API.Models.Validators
{
    /// <summary>
    /// User validation
    /// </summary>
    public class UserValidator:AbstractValidator<User>
    {
        /// <summary>
        /// constructor
        /// </summary>
        public UserValidator()
        {
            RuleFor(x => x.Name).MaximumLength(25).NotNull().NotEmpty().WithMessage("Name can not be empty");
            RuleFor(x => x.Surname).MaximumLength(25).NotNull().NotEmpty().WithMessage("Surname can not be empty");
            RuleFor(x => x.Age);

        }
    }
}
