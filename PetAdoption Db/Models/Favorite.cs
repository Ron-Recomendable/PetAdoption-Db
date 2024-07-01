using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption_Db.Models {

public class Favorite
{
    [Key] //Specifies that this property is the primary key.
    public int FavoriteId { get; set; }
    [ForeignKey("Pet")] //Specifies that this property is a foreign key referencing the Pet entity.
        public int PetId { get; set; }
    [ForeignKey("User")] //Specifies that this property is a foreign key referencing the User entity.
        public int UserId { get; set; }
    public Pet Pet { get; set; }
    public User User { get; set; }
}
}
