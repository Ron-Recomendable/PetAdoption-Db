using System.ComponentModel.DataAnnotations;

namespace PetAdoption_Db.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(50)]//This annotation makes this row not nullable and requires the user to input name. It also limits to 50 characters.
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        [DataType(DataType.MultilineText)]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Please provide description")]
        [StringLength(100, ErrorMessage = "Do not enter more than 100 characters")]//This annotation limits the characters in description to 100 characters.
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select which shelter")]
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        public ICollection<Application> Applications { get; set; }//This is a Foreign key referencing to Applications Table.
        public ICollection<Favorite> Favorites { get; set; }//This is a Foreign key referencing to Favorites Table.
    }
}