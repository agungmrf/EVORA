using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
}