using API.Models;

namespace API.DTOs.Cities;

public class CityDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid ProvinceGuid { get; set; }
    public Guid DistrictGuid { get; set; }
    
    public static explicit operator CityDto(City city) 
    {
        return new CityDto 
        {
            Guid = city.Guid,
            Name = city.Name,
            ProvinceGuid = city.ProvinceGuid,
            DistrictGuid = city.DisctrictGuid
        };
    }

    public static implicit operator City(CityDto cityDto) 
    {
        return new City
        {
            Guid = cityDto.Guid,
            Name = cityDto.Name,
            ProvinceGuid = cityDto.ProvinceGuid,
            DisctrictGuid = cityDto.DistrictGuid
        };
    }
}