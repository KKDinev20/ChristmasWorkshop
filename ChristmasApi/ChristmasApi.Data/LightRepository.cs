using ChristmasApi.Data.Models;

namespace ChristmasApi.Data;

public class LightRepository
{
    private readonly ChristmasApiDbContext dbContext;

    public LightRepository(ChristmasApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async void CreateLight(Light light)
    {
        await this.dbContext.Lights.AddAsync(light);
        await this.dbContext.SaveChangesAsync();
    }

    public IEnumerable<Light>GetLights()
    {
        return this.dbContext.Lights.ToList();
    }

    public void DeleteLight(Light light)
    {
        this.dbContext.Lights.Remove(light);
        this.dbContext.SaveChanges();
    }
}