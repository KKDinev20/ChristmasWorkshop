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

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddAuthorization();

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



        builder.Services.AddDbContext<LightContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddHttpClient();

        builder.Services.AddScoped<IValidationHandler, CoordinateValidationHandler>();
        builder.Services.AddScoped<IValidationService, ValidationService>();
        builder.Services.AddScoped<ILightFactory, LightFactory>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");
        app.UseAuthorization();
        app.MapControllers();


        app.Run();
    }
}