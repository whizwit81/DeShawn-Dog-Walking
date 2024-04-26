namespace DeShawn.Models.DTOs;

class DogsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
    public int WalkerId { get; set; }

     public WalkersDTO Walkers { get; set; }
     public CitiesDTO City { get; set; }

}
