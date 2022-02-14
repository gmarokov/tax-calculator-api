namespace Tax.Calculator.Api.Application.Calculator.Taxes;

public static class TaxSettings
{
    public const decimal MaxNoTaxThreshold = 1000;
    public const decimal IncomeTaxPercentage = 10;
    public const decimal SocialTaxPercentage = 15;
    public const decimal MaxSocialTaxThreshold = 3000 - MaxNoTaxThreshold;
    public const decimal MaxCharityPercentage = 10;
}
