namespace ChristmasApi.Main.Handlers;

using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

public class AttributeValidationHandler : IValidationHandler
{
    private IValidationHandler? next;

    public IValidationHandler SetNext(IValidationHandler next)
    {
        this.next = next;
        return next;
    }

    public Task<bool> ValidateAsync(Light light)
    {
        if (light.Radius < 3 || light.Radius > 6 || !this.IsValidColor(light.Color) || !this.IsValidEffect(light.Effects))
        {
            return Task.FromResult(false);
        }

        return this.next == null ? Task.FromResult(true) : this.next.ValidateAsync(light);
    }

    private bool IsValidColor(string color)
    {
        var validColors = new[] { "blue-lt", "blue-dk", "red", "gold-lt", "gold-dk" };
        return validColors.Contains(color);
    }

    private bool IsValidEffect(string effect)
    {
        var validEffects = new[] { "g1", "g2", "g3" };
        return validEffects.Contains(effect);
    }
}