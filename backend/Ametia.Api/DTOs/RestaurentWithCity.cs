using Grad.Controllers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Grad.DTO
{
    public class RestaurentWithCity
    {
        public int Id { get; set; }
        public string RestaurentName { get; set; } = null!;
        public int RestaurentOpiningHour { get; set; }
        public string TypeOfFood { get; set; } = null!;
        public int? RestaurentRating { get; set; }
        public string? RestaurentPhoneNumber { get; set; }
        public string CityName { get; set; } = null!;
        public RestaurentWithCity() { }
        public RestaurentWithCity(int id, string RestaurentName,int opn,string type_foodstring,int rat,string num ,string cityName)
        {
            this.Id= id;
            this.RestaurentName = RestaurentName;
            this.RestaurentOpiningHour= opn;
            this.TypeOfFood= type_foodstring;
            this.RestaurentRating= rat;
            this.RestaurentPhoneNumber= num;
            this.CityName = cityName;
        }
    }
}
