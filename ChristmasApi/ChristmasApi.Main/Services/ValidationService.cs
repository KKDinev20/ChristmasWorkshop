using ChristmasApi.Main.Handlers;

namespace ChristmasApi.Main.Services;

using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

public class ValidationService : IValidationService
{
    public async Task<bool> ValidateLightAsync(Light light)
    {
        Console.WriteLine("ValidateLightAsync");
        HttpClient client = new HttpClient();
        IValidationHandler coordinateHandler = new CoordinateValidationHandler(client);
        IValidationHandler colorHandler = new ColorValidationHandler();
        IValidationHandler effectHandler = new EffectValidationHandler();
        IValidationHandler radiusHandler = new RadiusValidationHandler();

        radiusHandler.SetNext(colorHandler).SetNext(effectHandler).SetNext(coordinateHandler);

        return await radiusHandler.ValidateAsync(light);
    }
}