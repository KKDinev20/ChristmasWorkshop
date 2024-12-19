using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

namespace ChristmasApi.Main.Handlers;

public class EffectValidationHandler : AbstractHandler
{
    public override async Task<bool> ValidateAsync(Light light)
    {
        var validEffects = new[] { "g1", "g2", "g3" };

        if (!validEffects.Contains(light.Effects))
        {
            return await Task.FromResult(false);
        }

        return await base.ValidateAsync(light);
    }
}