namespace Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;

public class IncomeTax : BaseTaxProcessor
{
    /// <inheritdoc />
    public override ITaxProcessorReport Calculate(ITaxProcessorReport report)
    {
        report.IncomeTax = (report.GrossIncome - TaxSettings.MaxNoTaxThreshold - report.CharitySubtraction)
            * TaxSettings.IncomeTaxPercentage / 100;
        return base.Calculate(report);
    }
}
