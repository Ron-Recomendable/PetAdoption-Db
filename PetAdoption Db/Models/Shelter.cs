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
        [Phone]
        public string Contact { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
