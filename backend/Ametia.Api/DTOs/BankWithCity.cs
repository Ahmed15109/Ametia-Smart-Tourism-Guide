using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace Grad.DTO
{
    public class BankWithCity
    {
        public int? Id { get; set; }
        public string? BankName { get; set; }
        public string? Image { get; set; }
        public int? Rating { get; set; }
        public string? ScialMedia { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? CityName { get; set; }
        public ICollection<Bank>?Banklist { get; set; }
    }
}
