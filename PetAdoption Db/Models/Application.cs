namespace PetAdoption_Db.Models

    public class Application
    {
    public int ApplicationID { get; set; }
    public int PetID { get; set; }
    public int UserID { get; set; }
    public string Status { get; set; }
    public DateTime ApplicationDate { get; set; }
    public User User { get; set; }
    public Pet Pet { get; set; }
}   
