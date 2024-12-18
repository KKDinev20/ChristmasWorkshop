namespace ChristmasApi.Main.Services;

using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

public class ValidationService : IValidationService
{
    private readonly IValidationHandler validationChain;

    public ValidationService(IValidationHandler validationChain)
    {
        this.validationChain = validationChain;
    }

    public async Task<bool> ValidateLightAsync(Light light)
    {
        return await this.validationChain.ValidateAsync(light);
    }
}