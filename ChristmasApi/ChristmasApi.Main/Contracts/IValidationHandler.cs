namespace ChristmasApi.Main.Contracts;

using ChristmasApi.Data.Models;

public interface IValidationHandler
{
    IValidationHandler SetNext(IValidationHandler next);

    Task<bool> ValidateAsync(Light light);
}