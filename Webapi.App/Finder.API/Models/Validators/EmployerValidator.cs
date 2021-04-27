using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finder.API.Models.Validators
{
    /// <summary>
    /// Validation Employer
    /// </summary>
    public class EmployerValidator:AbstractValidator<Employer>
    {
        /// <summary>
        /// constructor 
        /// </summary>
        public EmployerValidator()
        {
            RuleFor(x => x.Name).MaximumLength(25).NotNull().NotEmpty();
            RuleFor(x => x.UserId).NotNull();
        }

        
    }
}
