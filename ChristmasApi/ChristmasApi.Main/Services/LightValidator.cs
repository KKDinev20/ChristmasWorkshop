namespace ChristmasApi.Main.Services;

using System.Net.Http;
using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;
using ChristmasApi.Main.Validators;

public class LightValidatorService
{
    private readonly ILightValidator _validatorChain;

    public LightValidatorService(HttpClient httpClient)
    {
        var radiusValidator = new RadiusValidator();
        var coordinateValidator = new CoordinateValidator(httpClient);

        radiusValidator.SetNext(coordinateValidator);

        _validatorChain = radiusValidator;
    }

    public bool ValidateLight(Light light) => _validatorChain.Validate(light);
}