namespace DeShawn.Models.DTOs;

class WalkerCityDTO
{
    public int Id { get; set; }
    public int WalkerId { get; set; }
    public int CityId { get; set; }
    public List<WalkerCityDTO> WalkerCities { get; set; }

}


