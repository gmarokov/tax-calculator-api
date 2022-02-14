using FluentAssertions;
using Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;
using Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;
using Xunit;

namespace Tax.Calculator.Tests.Unit.TaxProcessors;

public class SocialTaxCalculatorTests
{
    [Fact]
    public void TaxCalculation_Should_Add_SocialTax_With_NoTaxThreshold_AndMaxSocialTaxThreshold()
    {
        //Arrange
        var report = new CalculatedTaxResponse { GrossIncome = 3400 };
        var socialTaxProcessor = new SocialTax();

        //Act
        var result = socialTaxProcessor.Calculate(report);

        //Assert
        result.SocialTax.Should().Be(300);
    }
}
