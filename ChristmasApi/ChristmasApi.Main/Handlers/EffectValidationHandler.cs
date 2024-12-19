using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

namespace ChristmasApi.Main.Handlers;

public class EffectValidationHandler : AbstractHandler
{
    public override async Task<bool> ValidateAsync(Light light)
    {
        Console.WriteLine("EFFECTS");
        var validEffects = new[] { "g1", "g2", "g3" };

        if (!validEffects.Contains(light.Effects))
        {
            Console.WriteLine("false effect");
            return await Task.FromResult(false);
        }

        Console.WriteLine("true effect");
        return await base.ValidateAsync(light);
    }
}