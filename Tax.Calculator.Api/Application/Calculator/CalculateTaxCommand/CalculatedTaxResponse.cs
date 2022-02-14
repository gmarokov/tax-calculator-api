using Tax.Calculator.Api.Application.Calculator.Taxes;

namespace Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;

public record CalculatedTaxResponse : ITaxProcessorReport
{
    /// <inheritdoc />
    public decimal GrossIncome { get; set; }

    /// <inheritdoc />
    public decimal CharitySpent { get; set; }

    /// <inheritdoc />
    public decimal CharitySubtraction { get; set; }

    /// <inheritdoc />
    public decimal IncomeTax { get; set; }

    /// <inheritdoc />
    public decimal SocialTax { get; set; }

    /// <inheritdoc />
    public decimal TotalTax => SocialTax + IncomeTax;

    /// <inheritdoc />
    public decimal NetIncome => GrossIncome - TotalTax;
}
