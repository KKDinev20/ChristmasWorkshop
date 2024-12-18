using ChristmasApi.Main.Requests;

namespace ChristmasApi.Main.Handlers;

using System.Net.Http.Json;
using ChristmasApi.Data.Models;

public class CoordinateValidationHandler : AbstractHandler
{
    private readonly HttpClient httpClient;

    public CoordinateValidationHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public override async Task<bool> ValidateAsync(Light light)
    {
        var response = await httpClient.PostAsJsonAsync(
            $"https://polygon.gsk567.com/?x={light.X}&y={light.Y}",
            new { desc = light.Description });

        Console.WriteLine($"Response: {response.StatusCode}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Coordinate validation failed: HTTP error.");
            return false;
        }

        var jsonResponse = await response.Content.ReadFromJsonAsync<CoordinateValidationResponse>();
        Console.WriteLine($"JSON Response: {System.Text.Json.JsonSerializer.Serialize(jsonResponse)}");

        if (jsonResponse != null && jsonResponse.In)
        {
            Console.WriteLine("Coordinate validation succeeded.");
            return await base.ValidateAsync(light);
        }

        Console.WriteLine("Coordinate validation failed: 'in' property not valid.");
        return false;
    }
}