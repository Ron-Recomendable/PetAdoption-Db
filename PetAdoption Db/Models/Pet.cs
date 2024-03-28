namespace PetAdoption_Db.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}