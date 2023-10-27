using API.Models;

namespace API.DTOs.PackageEvents;

public class PackageEventDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    
    public static explicit operator PackageEventDto(PackageEvent packageEvent)
    {
        return new PackageEventDto
        {
            Guid = packageEvent.Guid,
            Name = packageEvent.Name,
            Capacity = packageEvent.Capacity,
            Description = packageEvent.Description,
            Price = packageEvent.Price
        };
    }
    
    public static implicit operator PackageEvent(PackageEventDto packageEventDto)
    {
        return new PackageEvent
        {
            Guid = packageEventDto.Guid,
            Name = packageEventDto.Name,
            Capacity = packageEventDto.Capacity,
            Description = packageEventDto.Description,
            Price = packageEventDto.Price
        };
    }
}