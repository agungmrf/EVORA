using API.Models;

namespace API.DTOs.Districts;

public class CreateDistrictDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    
    public static implicit operator District(CreateDistrictDto createDistrictDto)
    {
        return new District
        {
            Guid = createDistrictDto.Guid,
            Name = createDistrictDto.Name,
        };
    }
}