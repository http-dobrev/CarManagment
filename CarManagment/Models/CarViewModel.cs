using System.ComponentModel.DataAnnotations;

namespace CarManagment.Models
{
    public class CarViewModel
    {
        public int Id {  get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Range(1950, 2100)]
        public int Year { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
    }
}
