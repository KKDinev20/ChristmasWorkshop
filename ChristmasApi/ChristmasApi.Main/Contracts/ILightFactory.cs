using ChristmasApi.Data.Models;

namespace ChristmasApi.Main.Contracts;

public interface ILightFactory
{
    Task<Light?> CreateLightAsync(string description, string christmasToken);
}