using ChristmasApi.Data.Models;

namespace ChristmasApi.Main.Contracts;

public interface ILightFactory
{
    Light CreateLight(string description, string christmasToken);
}