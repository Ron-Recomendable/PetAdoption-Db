using System.ComponentModel.DataAnnotations;

namespace PetAdoption_Db.Models
{
    public class Shelter
    {
        [Key]
        public int ShelterId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(50)]//This annotation can't be skipped, and limits the characters to 30 letters.
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        public string Location { get; set; }
        //[RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format")]
        [Required(ErrorMessage = "Please enter your mobile No.")]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
        public ICollection<Pet> Pets { get; set; }//This is a Foreign key referencing to Pets table.
    }
}
