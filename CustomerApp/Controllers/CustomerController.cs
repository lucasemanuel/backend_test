using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using CustomerApp.Services;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;
    private readonly AuthService _authService;

    public CustomerController(ICustomerRepository repository, IMapper mapper, AuthService authService)
    {
        _repository = repository;
        _mapper = mapper;
        _authService = authService;
    }

    [HttpPost]
    public ActionResult<ResponseCreateCustomerDTO> Create(RequestCreateCustomerDTO customerDTO)
    {
        var customer = _mapper.Map<Customer>(customerDTO);
        customer.password = _authService.HashPassword(customerDTO.password);
        _repository.Insert(customer);

        return Ok(_mapper.Map<ResponseCreateCustomerDTO>(customerDTO));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var customer = _repository.GetCustomerById(id);
        if (customer != null) {
            return Ok(_mapper.Map<ResponseCreateCustomerDTO>(customer));
        }
        
        return NotFound("Customer not found!");
    }
}