using API.Models;

namespace API.DTOs.Cities;

public class CreateCityDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid ProvinceGuid { get; set; }
    public Guid DistrictGuid { get; set; }
    
    public static implicit operator City(CreateCityDto createCityDto)
    {
        return new City 
        {
            Guid = createCityDto.Guid,
            Name = createCityDto.Name,
            ProvinceGuid = createCityDto.ProvinceGuid,
            DisctrictGuid = createCityDto.DistrictGuid,
        };
    }
}