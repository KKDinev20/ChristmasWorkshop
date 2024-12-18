namespace ChristmasApi.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Light
{
    [Required]
    public int Id { get; set; }

    [Range(0, double.MaxValue)]
    public double X { get; set; }

    [Range(0, double.MaxValue)]
    public double Y { get; set; }

    [Range(3, 6)]
    public double Radius { get; set; }

    [Required]
    [RegularExpression("blue-lt|blue-dk|red|gold-lt|gold-dk")]
    public required string Color { get; set; }

    [Required]
    [RegularExpression("g1|g2|g3")]
    public required string Effects { get; set; }

    [StringLength(500)]
    public required string Description { get; set; }

    [Required]
    public required string ChristmasToken { get; set; }
}