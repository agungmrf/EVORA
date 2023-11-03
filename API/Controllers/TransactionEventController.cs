using API.Contracts;
using API.DTOs.Locations;
using API.DTOs.TransactionEvents;
using API.Models;
using API.Utilities.Enums;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionEventController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPackageEventRepository _packageEventRepository;
    private readonly ICityRepository _cityRepository;
    public TransactionEventController(ITransactionRepository transactionRepository, ILocationRepository locationRepository, IEmailHandler emailHandler, ICustomerRepository customerRepository, 
        IPackageEventRepository packageEventRepository, ICityRepository cityRepository)
    {
        _transactionRepository = transactionRepository;
        _locationRepository = locationRepository;
        _emailHandler = emailHandler;
        _customerRepository = customerRepository;
        _packageEventRepository = packageEventRepository;
        _cityRepository = cityRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _transactionRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (TransactionEventDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<TransactionEventDto>>(data));
    }

    [HttpGet("detail")]
    public IActionResult GetAllDetail()
    {
        var result = _transactionRepository.GetAllDetailTransaction().OrderByDescending(t => t.Invoice);
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (TransactionDetailDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<TransactionDetailDto>>(data));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _transactionRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<TransactionEventDto>((TransactionEventDto)result));
    }

    [HttpGet("detailByGuid/{guid}")]
    public IActionResult DetailByGuidCustomer(Guid guid)
    {
        var result = _transactionRepository.DetailByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<TransactionDetaillAllDto>(result));
    }

    [HttpGet("GetByCustomer/{guid}")]
    public IActionResult GetByCustomer(Guid guid)
    {
        var result = _transactionRepository.GetByCustomer(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<IEnumerable<TransactionDetailDto>>(result));
    }


    [HttpPost]
    public IActionResult Create(TransactionEventDto transactionEventDto)
    {
        try
        {
            string currentYear = DateTime.Now.Year.ToString();
            TransactionEvent toCreate = transactionEventDto;
            toCreate.Invoice = GenerateHandler.Invoice(_transactionRepository.GetLastTransactionByYear(currentYear));
            var result = _transactionRepository.Create(toCreate);


            return Ok(new ResponseOKHandler<TransactionEventDto>("Data has been created successfully")
            { Data = (TransactionEventDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    [HttpPost("CreateOrder")]
    public IActionResult CreateOrder(InsertOrderTransactionDto createOrder)
    {
        using var context = _locationRepository.GetContext();
        using var transaction = context.Database.BeginTransaction();
        try
        {
            LocationDto toLocation = new LocationDto
            {
                Guid = Guid.NewGuid(),
                Street = createOrder.Street,
                District = createOrder.Disctrict,
                SubDistrict = createOrder.SubDistrict,
                CityGuid = createOrder.CityGuid
            };

            var cek = _locationRepository.Create(toLocation);
            if (cek is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create Location"));
            }

            string currentYear = DateTime.Now.Year.ToString();
            TransactionEventDto toOrder = new TransactionEventDto
            {
                CustomerGuid = createOrder.GuidCustomer,
                PackageGuid = createOrder.PackageGuid,
                LocationGuid = toLocation.Guid,
                Invoice = GenerateHandler.Invoice(_transactionRepository.GetLastTransactionByYear(currentYear)),
                EventDate = createOrder.EventDate,
                Status = (StatusTransaction)2,
                TransactionDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            TransactionEvent toCreate = toOrder;
            var result = _transactionRepository.Create(toCreate);
            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to Insert Transaction"));
            }
            // transaksi berhasil ditambahkan
            transaction.Commit();
            return Ok(new ResponseOKHandler<TransactionEventDto>("Data has been created successfully")
            { Data = (TransactionEventDto)result });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi kesalahan, lakukan rollback transaksi.
            transaction.Rollback();
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }


    [HttpPut]
    public IActionResult Update(TransactionEventDto transactionEventDto)
    {
        try
        {
            var entity = _transactionRepository.GetByGuid(transactionEventDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            TransactionEvent toUpdate = transactionEventDto;

            _transactionRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<TransactionEventDto>("Data has been updated successfully")
            { Data = (TransactionEventDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    [HttpPut("change-status")]
    public IActionResult ChangeStatus(ChangeTransactionStatusDto changeTransactionDto)
    {
        try
        {
            var entity = _transactionRepository.GetByGuid(changeTransactionDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            TransactionEvent toUpdate = entity;
            toUpdate.Status = changeTransactionDto.Status;

            _transactionRepository.Update(toUpdate);

            var getStatusKey = Enum.GetName(typeof(StatusTransaction), toUpdate.Status);
            var getCustomer = _customerRepository.GetByGuid(toUpdate.CustomerGuid);
            var getPackage = _packageEventRepository.GetByGuid(toUpdate.PacketEventGuid);
            var getLocation = _locationRepository.GetByGuid(toUpdate.LocationGuid);
            var getCity = _cityRepository.GetByGuid((Guid)getLocation.CityGuid);
            var bodyEmail = GenerateHandler.EmailTransactionTemplate(new TransactionDetailDto
            {
                FirstName = getCustomer.FirstName,
                LastName = getCustomer.LastName,
                Email = getCustomer.Email,
                Invoice = toUpdate.Invoice,
                EventDate = toUpdate.EventDate,
                Price = getPackage.Price,
                Package = getPackage.Name,
                Street = getLocation.Street,
                City = getCity.Name
            }, "We have change your order to <b>"+ getStatusKey + "</b>. The details of the order are below:");
            _emailHandler.Send(
                "Evora - Order Status",
                bodyEmail,
                getCustomer.Email);

            return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _transactionRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _transactionRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}