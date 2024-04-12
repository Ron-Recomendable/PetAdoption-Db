using System.ComponentModel.DataAnnotations;

namespace PetAdoption_Db.Models
{
    public class Shelter
    {
        [Key]
        public int ShelterId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Location { get; set; }
        [RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format")]
        public string Contact { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
