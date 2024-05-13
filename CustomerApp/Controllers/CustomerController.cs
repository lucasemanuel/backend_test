using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using CustomerApp.Entities;
using CustomerApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;

    public CustomerController(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    [HttpPost]
    public ActionResult<responseCreateCustomerDTO> Create(requestCreateCustomerDTO customerDTO)
    {
        var customer = _mapper.Map<Customer>(customerDTO);
        _repository.Insert(customer);

        return Ok(_mapper.Map<responseCreateCustomerDTO>(customerDTO));
    }

    // [HttpGet]
    // public IActionResult GetById(requestCreateCustomerDTO customerDTO)
    // {
    //     var customer = _mapper.Map<responseCreateCustomerDTO>(customerDTO);
    //     return Create(customer);
    // }
}