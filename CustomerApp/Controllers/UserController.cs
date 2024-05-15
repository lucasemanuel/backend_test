using AutoMapper;
using CustomerApp.DTOs.Output;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerApp.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;
    private readonly IBankAccountRepository _bankAccountRepository;

    public UserController(ICustomerRepository repository, IBankAccountRepository bankAccountRepository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _bankAccountRepository = bankAccountRepository;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<LoginUserDTO> GetUserInfo()
    {
        var email = User.FindFirst(ClaimTypes.Email).Value;
        var customer = _repository.GetCustomerByEmail(email);

        return Ok(_mapper.Map<LoginUserDTO>(customer));
    }

    
    [HttpGet("account")]
    [Authorize]
    public IActionResult getAccount()
    {
        var email = User.FindFirst(ClaimTypes.Email).Value;
        var customer = _repository.GetCustomerByEmail(email);
        var bankAccount = _bankAccountRepository.GetByCustomer(customer);
        if (bankAccount != null) {
            return Ok(bankAccount);
        }

        return NotFound("User not have account.");
    }
}