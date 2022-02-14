namespace Tax.Calculator.Api.Application.Calculator.Taxes;

public abstract class BaseTaxProcessor : ITaxProcessor
{
    private ITaxProcessor _nextProcessor;

    /// <inheritdoc />
    public ITaxProcessor SetNext(ITaxProcessor processor)
    {
        _nextProcessor = processor;
        return processor;
    }

    /// <inheritdoc />
    public virtual ITaxProcessorReport Calculate(ITaxProcessorReport report) =>
        _nextProcessor != null ? _nextProcessor.Calculate(report) : report;

}
