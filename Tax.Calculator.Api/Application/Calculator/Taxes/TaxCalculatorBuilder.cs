namespace Tax.Calculator.Api.Application.Calculator.Taxes;

/// <summary>
/// Builder for tax processors
/// </summary>
public class TaxCalculatorBuilder
{
    private ITaxProcessor _taxProcessor;
    private ITaxProcessor _firstProcessor;

    /// <summary>
    /// Adds Tax processor to the chain
    /// </summary>
    /// <param name="taxProcessor"></param>
    /// <returns></returns>
    public TaxCalculatorBuilder AddTaxProcessor(ITaxProcessor taxProcessor)
    {
        if (_taxProcessor == null)
        {
            _taxProcessor = _firstProcessor = taxProcessor;
        }
        else
        {
            _taxProcessor.SetNext(taxProcessor);
            _taxProcessor = taxProcessor;
        }

        return this;
    }

    /// <summary>
    /// Executes the chain with the order they are created
    /// </summary>
    /// <param name="report"></param>
    /// <returns></returns>
    public ITaxProcessorReport Execute(ITaxProcessorReport report) => _firstProcessor.Calculate(report);
}
