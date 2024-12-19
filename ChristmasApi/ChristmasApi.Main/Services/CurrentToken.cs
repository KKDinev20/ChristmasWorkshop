namespace ChristmasApi.Main.Services;

using ChristmasApi.Main.Contracts;
using Microsoft.AspNetCore.Http;

public class CurrentToken : ICurrentToken
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private static string? serverToken;

    public CurrentToken(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string? Value => serverToken;

    public static void UpdateToken(string newToken)
    {
        serverToken = newToken;
    }
}
