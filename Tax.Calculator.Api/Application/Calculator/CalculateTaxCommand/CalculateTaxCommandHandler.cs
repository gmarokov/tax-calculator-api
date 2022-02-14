using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Tax.Calculator.Api.Application.Calculator.Taxes;
using Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;

namespace Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;

public class CalculateTaxCommandHandler :  IRequestHandler<CalculateTaxCommand, Result<CalculatedTaxResponse>>
{
    private readonly IMemoryCache _memoryCache;

    public CalculateTaxCommandHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <inheritdoc />
    public async Task<Result<CalculatedTaxResponse>> Handle(CalculateTaxCommand request, CancellationToken cancellationToken)
    {
        if (!_memoryCache.TryGetValue(request.Ssn, out CalculatedTaxResponse? reportResponse))
        {
            reportResponse = new TaxCalculatorBuilder()
                    .AddTaxProcessor(new CharityTax())
                    .AddTaxProcessor(new IncomeTax())
                    .AddTaxProcessor(new SocialTax())
                    .Execute(new CalculatedTaxResponse { GrossIncome = request.GrossIncome, CharitySpent = request.CharitySpent})
                as CalculatedTaxResponse;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            _memoryCache.Set(request.Ssn, reportResponse, cacheEntryOptions);
        }

        return Result<CalculatedTaxResponse>.Success(reportResponse);
    }
}
