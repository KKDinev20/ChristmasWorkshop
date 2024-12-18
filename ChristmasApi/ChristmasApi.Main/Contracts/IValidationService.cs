

using ChristmasApi.Data.Models;

namespace ChristmasApi.Main.Contracts;

public interface IValidationService
{
    Task<bool> ValidateLightAsync(Light light);
}