namespace ChristmasApi.Main.Validators;

using System.Net.Http;
using System.Threading.Tasks;
using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

public class CoordinateValidator : ILightValidator
{
    private ILightValidator? _next;
    private readonly HttpClient _httpClient;

    public CoordinateValidator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void SetNext(ILightValidator next) => _next = next;

    public bool Validate(Light light)
    {
        var isValid = this.IsValidCoordinate(light.X, light.Y).Result;

        if (!isValid)
        {
            Console.WriteLine("Coordinate validation failed.");
            return false;
        }

        return _next?.Validate(light) ?? true;
    }

    private async Task<bool> IsValidCoordinate(double x, double y)
    {
        var response = await _httpClient.GetAsync($"https://polygon.gsk567.com/?x={x}&y={y}");
        return response.IsSuccessStatusCode;
    }
}