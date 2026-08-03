using Grad.Controllers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Grad.Models
{
    public class Embasse
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name Plz")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Enter Country Plz")]
        public string Country { get; set; } = null!;
        [Required(ErrorMessage = "Enter Working Houer Plz")]
        public int WorkingHours { get; set; }

        public string? ContactInfo { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        [ForeignKey("city")]
        public int CityId { get; set; }
        public City? city { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public byte[]? ImageBytes { get; set; }
    }
}
