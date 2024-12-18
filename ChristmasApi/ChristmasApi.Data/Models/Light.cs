using Newtonsoft.Json;

namespace ChristmasApi.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Light
{
    [Required]
    [JsonProperty("id")]
    public int Id { get; set; }

    [Range(0, double.MaxValue)]
    [JsonProperty("x")]
    public double X { get; set; }

    [Range(0, double.MaxValue)]
    [JsonProperty("y")]
    public double Y { get; set; }

    [Range(3, 6)]
    [JsonProperty("radius")]
    public double Radius { get; set; }

    [Required]
    [RegularExpression("blue-lt|blue-dk|red|gold-lt|gold-dk")]
    [JsonProperty("color")]
    public required string Color { get; set; }

    [Required]
    [RegularExpression("g1|g2|g3")]
    [JsonProperty("effects")]
    public required string Effects { get; set; }

    [StringLength(500)]
    [JsonProperty("desc")]
    public required string Description { get; set; }

    [Required]
    [JsonProperty("ct")]
    public required string ChristmasToken { get; set; }
}