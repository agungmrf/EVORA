using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProvinceController : ControllerBase
{
    private readonly IProvinceRepository _provinceRepository;

    public ProvinceController(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }
}