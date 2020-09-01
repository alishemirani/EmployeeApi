using System;
using EmployeeApi.DTO;
using FluentValidation;

namespace EmployeeApi.Validations
{
    public class EmployeeValidator : AbstractValidator<EmployeeDTO> {
        public EmployeeValidator() {
            RuleFor(t => t.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now.Date.AddYears(-18));

            RuleFor(t => t.DateOfHire)
                .GreaterThanOrEqualTo(DateTime.Now.Date);

            RuleFor(t => t.Name)
                .MinimumLength(3)
                .MaximumLength(150);
        }
    }
}