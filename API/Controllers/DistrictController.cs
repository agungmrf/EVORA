using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class DistrictController  : ControllerBase
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictController(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }
}