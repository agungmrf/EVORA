using API.DTOs.Cities;
using API.Models;

namespace API.DTOs.Districts;

public class DistrictDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid CityGuid { get; set; }

    public static explicit operator DistrictDto(District district) 
    {
        return new DistrictDto 
        {
            Guid = district.Guid,
            Name = district.Name,
            CityGuid = district.CityGuid

        };
    }

    public static implicit operator District(DistrictDto districtDto) 
    {
        return new District
        {
            Guid = districtDto.Guid,
            Name = districtDto.Name,
            CityGuid = districtDto.CityGuid
        };
    }
}