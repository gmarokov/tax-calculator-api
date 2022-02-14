using MediatR;

namespace Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;

public record CalculateTaxCommand : IRequest<Result<CalculatedTaxResponse>>
{
    /// <summary>
    /// Full name of the tax payer. Required.
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// 5 to 10 digits unique number per tax payer. Required.
    /// </summary>
    public long Ssn { get; init; }

    /// <summary>
    /// Gross income of the tax payer. Required.
    /// </summary>
    public decimal GrossIncome { get; init; }

    /// <summary>
    /// Annual charity spend amount. Required.
    /// </summary>
    public decimal CharitySpent { get; init; }
}
