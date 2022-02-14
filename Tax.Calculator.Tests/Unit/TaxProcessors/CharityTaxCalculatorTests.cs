using FluentAssertions;
using Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;
using Tax.Calculator.Api.Application.Calculator.Taxes.TaxProcessors;
using Xunit;

namespace Tax.Calculator.Tests.Unit.TaxProcessors;

public class CharityTaxCalculatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(1000)]
    public void TaxCalculation_Should_NotApply_When_GrossIncome_IsWithin_Threshold(decimal grossIncome)
    {
        //Arrange
        var report = new CalculatedTaxResponse { GrossIncome = grossIncome };
        var charityTaxProcessor = new CharityTax();

        //Act
        var result = charityTaxProcessor.Calculate(report);

        //Assert
        result.NetIncome.Should().Be(report.GrossIncome);
    }

    [Fact]
    public void TaxCalculation_Should_Subtract_When_Charity_Spent_Within_Limit()
    {
        //Arrange
        var report = new CalculatedTaxResponse { GrossIncome = 3600, CharitySpent = 330 };
        var charityTaxProcessor = new CharityTax();

        //Act
        var result = charityTaxProcessor.Calculate(report);

        //Assert
        result.CharitySubtraction.Should().Be(report.CharitySpent);
    }

    [Fact]
    public void TaxCalculation_Should_Subtract_ToThreshold_When_Charity_Spent_AboveLimit()
    {
        //Arrange
        var report = new CalculatedTaxResponse { GrossIncome = 3600, CharitySpent = 520 };
        var charityTaxProcessor = new CharityTax();

        //Act
        var result = charityTaxProcessor.Calculate(report);

        //Assert
        result.CharitySubtraction.Should().Be(360);
    }
}
