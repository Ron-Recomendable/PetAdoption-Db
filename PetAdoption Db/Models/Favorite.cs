namespace PetAdoption_Db.Models

public class Favorite
{
    public int FavoriteId { get; set; }
    public int PetId { get; set; }
    public int UserId { get; set; }
    public Pet Pet { get; set; }
}