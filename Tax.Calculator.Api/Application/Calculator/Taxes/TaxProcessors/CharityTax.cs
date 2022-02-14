namespace Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;

public class CharityTax : BaseTaxProcessor
{
    /// <inheritdoc />
    public override ITaxProcessorReport Calculate(ITaxProcessorReport report)
    {
        if (report.GrossIncome <= TaxSettings.MaxNoTaxThreshold)
        {
            return report;
        }

        if (report.CharitySpent > 0)
        {
            var maxCharityTaxFreeAmount = report.GrossIncome * TaxSettings.MaxCharityPercentage / 100;
            report.CharitySubtraction = report.CharitySpent > maxCharityTaxFreeAmount
                ? maxCharityTaxFreeAmount
                : report.CharitySpent;
        }

        return base.Calculate(report);
    }
}
