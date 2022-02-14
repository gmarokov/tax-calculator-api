using FluentAssertions;
using Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;
using Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;
using Xunit;

namespace Tax.Calculator.Tests.Unit.TaxProcessors;

public class IncomeTaxCalculatorTests
{
    [Fact]
    public void TaxCalculation_Should_Add_IncomeTax_Subtracted_NoTaxThreshold()
    {
        //Arrange
        var report = new CalculatedTaxResponse { GrossIncome = 2500, CharitySubtraction = 150 };
        var incomeTaxProcessor = new IncomeTax();

        //Act
        var result = incomeTaxProcessor.Calculate(report);

        //Assert
        result.IncomeTax.Should().Be(135m);
    }
}
