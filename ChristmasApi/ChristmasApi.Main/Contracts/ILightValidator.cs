namespace ChristmasApi.Main.Contracts;

using ChristmasApi.Data.Models;

public interface ILightValidator
{
    void SetNext(ILightValidator next);
    bool Validate(Light light);
}