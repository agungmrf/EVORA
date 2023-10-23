using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EquipmentController : ControllerBase
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentController(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }
}