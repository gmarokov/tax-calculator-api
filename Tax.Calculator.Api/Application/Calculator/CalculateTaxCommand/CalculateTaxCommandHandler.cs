using MediatR;
using Tax.Calculator.Api.Application.Calculator.Taxes;
using Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;

namespace Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;

public class CalculateTaxCommandHandler :  IRequestHandler<CalculateTaxCommand, Result<CalculatedTaxResponse>>
{
    /// <inheritdoc />
    public async Task<Result<CalculatedTaxResponse>> Handle(CalculateTaxCommand request, CancellationToken cancellationToken)
    {
        //TODO: Check memory cache first

        var reportResponse = new TaxCalculatorBuilder()
            .AddTaxProcessor(new CharityTax())
            .AddTaxProcessor(new IncomeTax())
            .AddTaxProcessor(new SocialTax())
            .Execute(new CalculatedTaxResponse { GrossIncome = request.GrossIncome, CharitySpent = request.CharitySpent})
            as CalculatedTaxResponse;

        //TODO: Set in cache
        return Result<CalculatedTaxResponse>.Success(reportResponse);
    }
}
