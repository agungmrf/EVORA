using API.Models;

namespace API.DTOs.Provinces;

public class ProvinceDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    
    public static explicit operator ProvinceDto(Province province)
    {
        return new ProvinceDto
        {
            Guid = province.Guid,
            Name = province.Name,
        };
    }
    
    public static implicit operator Province(ProvinceDto provinceDto)
    {
        return new Province
        {
            Guid = provinceDto.Guid,
            Name = provinceDto.Name,
        };
    }
}