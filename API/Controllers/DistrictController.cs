using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DistrictController  : ControllerBase
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictController(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }
}