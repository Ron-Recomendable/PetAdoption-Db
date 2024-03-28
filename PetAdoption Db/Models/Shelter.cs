namespace PetAdoption_Db.Models
{
    public class Shelter
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
