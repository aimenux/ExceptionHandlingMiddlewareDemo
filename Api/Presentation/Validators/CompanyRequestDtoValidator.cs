using Api.Presentation.ViewModels;
using FluentValidation;

namespace Api.Presentation.Validators
{
    public class CompanyRequestDtoValidator : AbstractValidator<CompanyRequestDto>
    {
        public CompanyRequestDtoValidator()
        {
            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(10);
        }
    }
}
