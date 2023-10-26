using API.Models;

namespace API.DTOs.Locations;

public class LocationDto
{
    public Guid Guid { get; set; }
    public string Street { get; set; }
    public Guid SubDistrictGuid { get; set; } 
    
    public static explicit operator LocationDto(Location location)
    {
        return new LocationDto
        {
            Guid = location.Guid,
            Street = location.Street,
            SubDistrictGuid = location.SubDistrictGuid
        };
    }

    public static implicit operator Location(LocationDto locationDto)
    {
        return new Location
        {
            Guid = locationDto.Guid,
            Street = locationDto.Street,
            SubDistrictGuid = locationDto.SubDistrictGuid
        };
    }
}