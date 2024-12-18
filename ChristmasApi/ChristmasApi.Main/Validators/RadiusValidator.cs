namespace ChristmasApi.Main.Validators;

using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

public class RadiusValidator : ILightValidator
{
    private ILightValidator? _next;

    public void SetNext(ILightValidator next) => _next = next;

    public bool Validate(Light light)
    {
        if (light.Radius < 3 || light.Radius > 6)
        {
            Console.WriteLine("Radius validation failed.");
            return false;
        }

        return _next?.Validate(light) ?? true;
    }
}