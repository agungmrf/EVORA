using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EventEquipmentController : ControllerBase
{
    private readonly IEventEquipmentRepository _eventEquipmentRepository;

    public EventEquipmentController(IEventEquipmentRepository eventEquipmentRepository)
    {
        _eventEquipmentRepository = eventEquipmentRepository;
    }
}