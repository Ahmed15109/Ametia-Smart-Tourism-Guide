using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace Grad.Models
{
    public class Type_place
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Name of type of place plz")]
        public string Name { get; set; } = null!;
        public ICollection<Tourismt_Place>? tourismt_Places { get; set; }
    }
}
