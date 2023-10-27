using API.Models;

namespace API.DTOs.SubDistricts;

public class SubDistrictDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid DistrictGuid { get; set; }
    
    public static explicit operator SubDistrictDto(SubDistrict subDistrict)
    {
        return new SubDistrictDto
        {
            Guid = subDistrict.Guid,
            Name = subDistrict.Name,
            DistrictGuid = subDistrict.DistrictGuid
        };
    }
    
    public static implicit operator SubDistrict(SubDistrictDto subDistrictDto)
    {
        return new SubDistrict
        {
            Guid = subDistrictDto.Guid,
            Name = subDistrictDto.Name,
            DistrictGuid = subDistrictDto.DistrictGuid
        };
    }
}