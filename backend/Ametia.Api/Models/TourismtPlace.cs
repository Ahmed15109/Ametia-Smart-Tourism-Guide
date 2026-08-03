using Grad.Controllers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Grad.Models;

namespace WebApplication4.Models
{
    public class Tourismt_Place
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name Plz")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Enter Discription Plz")]
        public string ? Discription { get; set; }
        public float ? TicketPrice { get; set; }
        public int? Rating { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        [ForeignKey("city")]
        public int CityId { get; set; }
        public City? city { get; set; }
        [ForeignKey("type_Place")]
        public int  Typeofplaceid { get; set; }

        public Type_place ? type_Place { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public byte[]? ImageBytes { get; set; }

    }
}
