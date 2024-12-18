using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

namespace ChristmasApi.Main.Handlers;

public class ColorValidationHandler : AbstractHandler
{
    public override async Task<bool> ValidateAsync(Light light)
    {
        var validColors = new[] { "blue-lt", "blue-dk", "red", "gold-lt", "gold-dk" };
        if (!validColors.Contains(light.Color))
        {
            return await Task.FromResult(false);
        }
        return await base.ValidateAsync(light);
    }
}