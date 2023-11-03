using API.Models;

namespace API.DTOs.Locations;

public class LocationCompleteDto
{
    public Guid Guid { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string SubDistrict { get; set; }

    public static explicit operator LocationCompleteDto(Location location)
    {
        return new LocationCompleteDto
        {
            Guid = location.Guid,
            Street = location.Street,
            District = location.District,
            SubDistrict = location.SubDistrict,
        };
    }

    public static implicit operator Location(LocationCompleteDto locationDto)
    {
        return new Location
        {
            Guid = locationDto.Guid,
            Street = locationDto.Street,
            District = locationDto.District,
            SubDistrict = locationDto.SubDistrict,
        };
    }
}