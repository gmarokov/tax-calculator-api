using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Tax.Calculator.Api.Application.Calculator.CalculateTaxCommand;
using Xunit;

namespace Tax.Calculator.Tests.Integration;

public class CalculatorEndpointTests
{
    private const string CalculatorEndpoint = "calculator";

    private readonly HttpClient _client;

    public CalculatorEndpointTests()
    {
        var application = new WebApplicationFactory<Program>();
        _client = application.CreateClient();
    }

    [Theory]
    [InlineData(3600, 524, 3076)]
    [InlineData(2500, 150, 2162.5)]
    [InlineData(3400, 0, 2860)]
    [InlineData(980, 0, 980)]
    public async Task Post_Calculate_Should_Return_Successful_Response(
        decimal grossIncome, decimal charitySpent, decimal expectedNetIncome)
    {
        //Arrange
        var requestBodyContent = new Dictionary<string, string>
        {
            {"fullName", "GeorgiM"},
            {"ssn", $"1903{grossIncome}"},
            {"grossIncome", grossIncome.ToString(CultureInfo.InvariantCulture)},
            {"charitySpent", charitySpent.ToString(CultureInfo.InvariantCulture)},
        };

        //Act
        var response = await _client.PostAsync(CalculatorEndpoint, CreateJsonHttpContent(requestBodyContent));

        //Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var taxReport = await response.Content.ReadAsAsync<CalculatedTaxResponse>();
        taxReport.Should().NotBeNull();
        taxReport.NetIncome.Should().Be(expectedNetIncome);
    }

    [Fact]
    public async Task Post_Calculate_Should_Return_BadRequest_When_Model_NotValid()
    {
        //Arrange
        var requestBodyContent = new Dictionary<string, string>
        {
            {"fullName", ""},
            {"ssn", "1"},
            {"grossIncome", "0"},
            {"charitySpent", "0"},
        };

        //Act
        var response = await _client.PostAsync(CalculatorEndpoint, CreateJsonHttpContent(requestBodyContent));

        //Assert
        var responseContent = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseContent.Should().NotBeEmpty();
    }

    private static StringContent CreateJsonHttpContent(Dictionary<string, string> content) =>
        new(
            JsonConvert.SerializeObject(content),
            Encoding.UTF8,
            "application/json"
        );
}
