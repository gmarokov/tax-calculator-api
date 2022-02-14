namespace Tax.Calculator.Api.Application.Calculator.Taxes;

public interface ITaxProcessorReport
{
    /// <summary>
    /// The amount of the gross income.
    /// </summary>
    public decimal GrossIncome { get; set; }

    /// <summary>
    /// The amount of the charity spent.
    /// </summary>
    public decimal CharitySpent { get; set; }

    /// <summary>
    /// The amount which will be subtracted from taxes.
    /// </summary>
    public decimal CharitySubtraction { get; set; }

    /// <summary>
    /// The amount of the income tax. 10% is incurred to the excess (amount above 1000).
    /// </summary>
    public decimal IncomeTax { get; set; }

    /// <summary>
    /// The amount of the social contributions. 15% whatever is above 1000 IDR but never apply to amounts higher than 3000.
    /// </summary>
    public decimal SocialTax { get; set; }

    /// <summary>
    /// The amount of total taxes to be paid.
    /// </summary>
    public decimal TotalTax { get; }

    /// <summary>
    /// The amount remaining for the tax payer after the taxes
    /// </summary>
    public decimal NetIncome { get; }
}
