using Grad.Models;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace Grad.Controllers
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter City Plz")]
        public string Name { get; set; } = null!;
        public ICollection<Bank>? Banks { get; set; }
        public ICollection<Embasse>? Embasses { get; set; }
        public ICollection<EntertainmentPlace>? EntertainmentPlaces { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
        public ICollection<Restaurant>? Restaurants { get; set; }
        public ICollection<Tourismt_Place>? Tourismt_Places { get; set; }
        public ICollection<TransportProvider>? TransportProviders { get; set; }

    }
}
