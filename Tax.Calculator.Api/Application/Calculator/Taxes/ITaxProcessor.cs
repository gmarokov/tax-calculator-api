namespace Tax.Calculator.Api.Application.Calculator.Taxes;

public interface ITaxProcessor
{
    /// <summary>
    /// Sets the next tax in the chain
    /// </summary>
    /// <param name="processor"><see cref="ITaxProcessor"/></param>
    /// <returns><see cref="ITaxProcessor"/></returns>
    ITaxProcessor SetNext(ITaxProcessor processor);

    /// <summary>
    /// Calculate
    /// </summary>
    /// <param name="report"></param>
    /// <returns></returns>
    ITaxProcessorReport Calculate(ITaxProcessorReport report);
}






