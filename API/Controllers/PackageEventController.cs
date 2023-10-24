using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class PackageEventController : ControllerBase
{
    private readonly IPackageEventRepository _packageEventRepository;

    public PackageEventController(IPackageEventRepository packageEventRepository)
    {
        _packageEventRepository = packageEventRepository;
    }
}