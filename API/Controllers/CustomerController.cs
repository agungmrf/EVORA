using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
}