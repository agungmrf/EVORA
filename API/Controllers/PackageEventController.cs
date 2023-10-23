using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PackageEventController : ControllerBase
{
    private readonly IPackageEventRepository _packageEventRepository;

    public PackageEventController(IPackageEventRepository packageEventRepository)
    {
        _packageEventRepository = packageEventRepository;
    }
}