using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption_Db.Models
{

    public class Application
    {
        [Key] //Specifies that this property is the primary key.
        public int ApplicationID { get; set; }
        [ForeignKey("Pet")] //Specifies that this property is a foreign key referencing the Pet entity.
        public int PetID { get; set; }
        [ForeignKey("User")] //Specifies that this property is a foreign key referencing the User entity.
        public int UserID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Status can't be longer than 20 characters.")] //Limits the length of the status to 20 characters and provides a custom error message.
        public string Status { get; set; }
        [Required]
        [DataType(DataType.DateTime)] //Specifies the data type as DateTime.
        public DateTime ApplicationDate { get; set; }
        public User User { get; set; }
        public Pet Pet { get; set; }
    }
}

