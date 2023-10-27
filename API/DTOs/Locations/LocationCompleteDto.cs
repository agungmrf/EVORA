using API.Models;

namespace API.DTOs.Locations;

public class LocationCompleteDto
{
    public Guid Guid { get; set; }
    public string Street { get; set; }
    public Guid SubDistrictGuid { get; set; } 
    
    public static explicit operator LocationCompleteDto(Location location)
    {
        return new LocationCompleteDto
        {
            Guid = location.Guid,
            Street = location.Street,
            SubDistrictGuid = location.SubDistrictGuid
        };
    }

    public static implicit operator Location(LocationCompleteDto locationDto)
    {
        return new Location
        {
            Guid = locationDto.Guid,
            Street = locationDto.Street,
            SubDistrictGuid = locationDto.SubDistrictGuid
        };
    }
}