using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SubDistrictController : ControllerBase
{
    private readonly ISubDistrictRepository _subDistrictRepository;

    public SubDistrictController(ISubDistrictRepository subDistrictRepository)
    {
        _subDistrictRepository = subDistrictRepository;
    }
}