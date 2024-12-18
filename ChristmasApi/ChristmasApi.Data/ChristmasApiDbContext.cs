using ChristmasApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristmasApi.Data;

public class ChristmasApiDbContext : DbContext
{
    public ChristmasApiDbContext(DbContextOptions<ChristmasApiDbContext> options) : base(options)
    {
    }

    public DbSet<Light> Lights { get; set; }
}