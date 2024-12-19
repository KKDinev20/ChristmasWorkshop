namespace ChristmasApi.Main;

using ChristmasApi.Data;
using ChristmasApi.Main.Contracts;
using ChristmasApi.Main.Handlers;
using ChristmasApi.Main.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("https://codingburgas.karagogov.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithHeaders("Access-Control-Allow-Private-Network");
            });
        });

        builder.Services.AddDbContext<ChristmasApiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddAuthorization();

        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<AbstractHandler, CoordinateValidationHandler>();
        builder.Services.AddScoped<AbstractHandler, ColorValidationHandler>();
        builder.Services.AddScoped<AbstractHandler, EffectValidationHandler>();
        builder.Services.AddScoped<AbstractHandler, RadiusValidationHandler>();

        builder.Services.AddScoped<ILightFactory, LightFactory>();
        builder.Services.AddScoped<IValidationService, ValidationService>();

        builder.Services.AddScoped<ICurrentToken, CurrentToken>();
        builder.Services.AddHostedService<ChristmasTokenService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowSpecificOrigin");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}