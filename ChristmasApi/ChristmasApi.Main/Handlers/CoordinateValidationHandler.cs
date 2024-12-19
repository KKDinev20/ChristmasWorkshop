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
        var response = await this.httpClient.PostAsJsonAsync(
            $"https://polygon.gsk567.com/?x={light.X}&y={light.Y}",
            new { desc = light.Description });

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var jsonResponse = await response.Content.ReadFromJsonAsync<CoordinateValidationResponse>();
        Console.WriteLine($"JSON Response: {System.Text.Json.JsonSerializer.Serialize(jsonResponse)}");

        if (jsonResponse != null && jsonResponse.In)
        {
            return await base.ValidateAsync(light);
        }

        var (x, y) = this.GenerateRandomPoint();
        light.X = x;
        light.Y = y;
        return await this.ValidateAsync(light);
    }

    private (double X, double Y) GenerateRandomPoint()
    {
        Random random = new Random();
        double cornerOneX = 0.00, cornerOneY = 170.30;
        double cornerTwoX = 125.80, cornerTwoY = 170.30;
        double cornerThreeX = 62.80, cornerThreeY = 14.90;

        double radiusOne = random.NextDouble();
        double radiusTwo = random.NextDouble();

        double sqrtR1 = Math.Sqrt(radiusOne);
        double x = ((1 - sqrtR1) * cornerOneX) + ((sqrtR1 * (1 - radiusTwo)) * cornerTwoX) + ((sqrtR1 * radiusTwo) * cornerThreeX);
        double y = ((1 - sqrtR1) * cornerOneY) + ((sqrtR1 * (1 - radiusTwo)) * cornerTwoY) + ((sqrtR1 * radiusTwo) * cornerThreeY);

        return (x, y);
    }
}