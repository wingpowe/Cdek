


using System.ComponentModel.DataAnnotations;

namespace Cdek.Models
{
    public class Cargo
    {
        public long Id { get; set; }
        [Required] public double Weight { get; set; }
        [Required] public double Height { get; set; }
        [Required] public double Width { get; set; }
        [Required] public double Length { get; set; }
        [Required] public string? FromLocation { get; set; }
        [Required] public string? ToLocation { get; set; }

    }

}

