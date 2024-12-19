using ChristmasApi.Data;
using ChristmasApi.Main.Contracts;

public class ChristmasTokenService : BackgroundService
{
    private readonly ILogger<ChristmasTokenService> logger;
    private readonly IServiceProvider serviceProvider;

    public ChristmasTokenService(
        ILogger<ChristmasTokenService> logger,
        IServiceProvider serviceProvider)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = this.serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ChristmasApiDbContext>();
                    var currentToken = scope.ServiceProvider.GetRequiredService<ICurrentToken>();

                    var lights = dbContext.Lights.ToList();
                    foreach (var light in lights)
                    {
                        if (light.ChristmasToken != currentToken.Value)
                        {
                            dbContext.Lights.Remove(light);
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Christmas tokens validation error");
            }
        }
    }
}
