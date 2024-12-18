namespace ChristmasApi.Main.Services;

using ChristmasApi.Main.Contracts;
using ChristmasApi.Data.Models;

public class LightFactory : ILightFactory
{
    private readonly Random random = new Random();
    private readonly IValidationService validationService;

    public LightFactory(IValidationService validationService)
    {
        this.validationService = validationService;
    }

    public async Task<Light?> CreateLightAsync(string description, string christmasToken)
    {
        var (x, y) = this.GenerateRandomPoint();

        var light = new Light
        {
            X = x,
            Y = y,
            Radius = (this.random.NextDouble() * (6 - 3)) + 3,
            Color = this.GetRandomColor(),
            Effects = this.GetRandomEffect(),
            Description = description,
            ChristmasToken = christmasToken,
        };

        var isValid = await this.validationService.ValidateLightAsync(light);
        return isValid ? light : null;
    }

    private (double X, double Y) GenerateRandomPoint()
    {
        double cornerOneX = 0.00, cornerOneY = 170.30;
        double cornerTwoX = 125.80, cornerTwoY = 170.30;
        double cornerThreeX = 62.80, cornerThreeY = 14.90;

        double radiusOne = this.random.NextDouble();
        double radiusTwo = this.random.NextDouble();

        double sqrtR1 = Math.Sqrt(radiusOne);
        double x = ((1 - sqrtR1) * cornerOneX) + ((sqrtR1 * (1 - radiusTwo)) * cornerTwoX) + ((sqrtR1 * radiusTwo) * cornerThreeX);
        double y = ((1 - sqrtR1) * cornerOneY) + ((sqrtR1 * (1 - radiusTwo)) * cornerTwoY) + ((sqrtR1 * radiusTwo) * cornerThreeY);

        return (x, y);
    }

    private string GetRandomColor()
    {
        var colors = new[] { "blue-lt", "blue-dk", "red", "gold-lt", "gold-dk" };
        return colors[this.random.Next(colors.Length)];
    }

    private string GetRandomEffect()
    {
        var effects = new[] { "g1", "g2", "g3" };
        return effects[this.random.Next(effects.Length)];
    }
}
