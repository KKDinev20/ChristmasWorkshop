using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

namespace ChristmasApi.Main.Handlers;

public class RadiusValidationHandler : AbstractHandler
{
    public override async Task<bool> ValidateAsync(Light light)
    {
        if (light.Radius < 3 || light.Radius > 6)
        {
            Random random = new Random();
            double radius = 0;
            while (radius < 3 || radius > 6)
            {
                radius = (random.NextDouble() * (6 - 3)) + 3;
            }

            light.Radius = radius;
        }

        return await base.ValidateAsync(light);
    }
}