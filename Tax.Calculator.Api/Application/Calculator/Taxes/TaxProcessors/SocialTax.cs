namespace Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;

public class SocialTax : BaseTaxProcessor
{
    /// <inheritdoc />
    public override ITaxProcessorReport Calculate(ITaxProcessorReport report)
    {
        var socialTaxBase = report.GrossIncome - TaxSettings.MaxNoTaxThreshold;
        var amountToSocialTax = socialTaxBase > TaxSettings.MaxSocialTaxThreshold
            ? TaxSettings.MaxSocialTaxThreshold
            : socialTaxBase - report.CharitySubtraction;

        report.SocialTax = amountToSocialTax * TaxSettings.SocialTaxPercentage / 100;
        return base.Calculate(report);
    }
}
