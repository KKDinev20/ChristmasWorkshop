using System.ComponentModel.DataAnnotations;
namespace ChristmasApi.Data.Models;

public class Light
{
    [Required]
    public int Id { get; set; } 
    
    [Range(0, int.MaxValue, ErrorMessage = "X coordinate must be a non-negative integer.")]
    public int X { get; set; } // Horizontal coordinate
    
    [Range(0, int.MaxValue, ErrorMessage = "Y coordinate must be a non-negative integer.")]
    public int Y { get; set; } // Vertical coordinate
    
    [Range(3, 6, ErrorMessage = "Radius must be between 3 and 6.")]
    public double Radius { get; set; } // Light radius
    
    [Required]
    [RegularExpression("blue-lt|blue-dk|red|gold-lt|gold-dk", ErrorMessage = "Invalid color. Valid values are: blue-lt, blue-dk, red, gold-lt, gold-dk.")]
    public string Color { get; set; } // Light color
    
    [Required]
    [RegularExpression("g1|g2|g3", ErrorMessage = "Invalid effect. Valid values are: g1, g2, g3.")]
    public string Effects { get; set; } // Light effects
    
    [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
    public string Desc { get; set; } // Description (provided from HTTP POST request body)
    
    [Required]
    [StringLength(500, ErrorMessage = "Christmas-Token must be less than 500 characters.")]
    public string Ct { get; set; } // Christmas-Token (provided from HTTP POST request header)
}