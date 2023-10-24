using API.Models;

namespace API.DTOs.Provinces;

public class CreateProvinceDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    
    public static implicit operator Province(CreateProvinceDto createProvinceDto)
    {
        return new Province
        {
            Guid = createProvinceDto.Guid,
            Name = createProvinceDto.Name,
        };
    }
}