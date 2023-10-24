using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class SubDistrictController : ControllerBase
{
    private readonly ISubDistrictRepository _subDistrictRepository;

    public SubDistrictController(ISubDistrictRepository subDistrictRepository)
    {
        _subDistrictRepository = subDistrictRepository;
    }
}