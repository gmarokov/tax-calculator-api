using FluentValidation;

namespace Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;

public class CalculateTaxCommandValidator : AbstractValidator<CalculateTaxCommand>
{
    public CalculateTaxCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Ssn).NotEqual(0).InclusiveBetween(10000, 9999999999);
        RuleFor(x => x.GrossIncome).NotEqual(0).NotEmpty();
    }
}
