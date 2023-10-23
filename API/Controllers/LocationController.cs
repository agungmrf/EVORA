using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
}