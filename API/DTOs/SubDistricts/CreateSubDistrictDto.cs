using API.Models;

namespace API.DTOs.SubDistricts;

public class CreateSubDistrictDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid DistrictGuid { get; set; }
    
    public static implicit operator SubDistrict(CreateSubDistrictDto createSubDistrictDto)
    {
        return new SubDistrict
        {
            Guid = createSubDistrictDto.Guid,
            Name = createSubDistrictDto.Name,
            DistrictGuid = createSubDistrictDto.DistrictGuid
        };
    }
}