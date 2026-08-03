using Grad.Models;
using WebApplication4.Models;

namespace Grad.DTO
{
    public class CityWithAll
    {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public List<Bank>? Banks { get; set; } = new();
        public ICollection<Embasse>? Embasses { get; set; }
        public ICollection<EntertainmentPlace>? EntertainmentPlaces { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
        public ICollection<Restaurant>? Restaurants { get; set; }
        public ICollection<Tourismt_Place>? Tourismt_Places { get; set; }
        public ICollection<TransportProvider>? TransportProviders { get; set; }

    }
}
