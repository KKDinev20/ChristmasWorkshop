using ChristmasApi.Data.Models;

namespace ChristmasApi.Data;

using Microsoft.EntityFrameworkCore;

public class LightContext : DbContext
{
    public LightContext(DbContextOptions<LightContext> options) : base(options)
    {
    }

    public DbSet<Light> Lights { get; set; }
}