using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using FluentValidation;

namespace Business.Dtos.Validators
{
    public class EmployeeRegFormValidator : AbstractValidator<EmployeeRegForm>
    {
        public EmployeeRegFormValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.")

                .WithMessage("Email already exists.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").WithMessage("Invalid phone number.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Role ID must be greater than 0.");
        }
    }

}


