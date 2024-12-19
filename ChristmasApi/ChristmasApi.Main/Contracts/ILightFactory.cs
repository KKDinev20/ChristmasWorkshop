namespace ChristmasApi.Main.Contracts;

using ChristmasApi.Data.Models;

public interface ILightFactory
{
    Task<Light?> CreateLightAsync(string description);
}