namespace ChristmasApi.Main.Handlers;

using System.Net.Http.Json;
using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

public class CoordinateValidationHandler : IValidationHandler
{
    private readonly HttpClient httpClient;
    private IValidationHandler? next;

    public CoordinateValidationHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public IValidationHandler SetNext(IValidationHandler next)
    {
        this.next = next;
        return next;
    }

    public async Task<bool> ValidateAsync(Light light)
    {
        var response = await this.httpClient.PostAsJsonAsync(
            $"https://polygon.gsk567.com/?x={light.X}&y={light.Y}",
            new { desc = light.Description });

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            if (jsonResponse != null && jsonResponse.TryGetValue("succeeded", out var succeeded) && succeeded is bool isValid && isValid)
            {
                return this.next == null || await this.next.ValidateAsync(light);
            }
        }

        return false;
    }
}