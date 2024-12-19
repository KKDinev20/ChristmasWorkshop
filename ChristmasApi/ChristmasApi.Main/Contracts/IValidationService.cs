namespace ChristmasApi.Main.Contracts;

using ChristmasApi.Data.Models;

public interface IValidationService
{
    Task<bool> ValidateLightAsync(Light light);
}