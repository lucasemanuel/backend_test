using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using CustomerApp.Services;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public AuthController(ICustomerRepository customerRepository, AuthService authService, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDTO loginDTO)
    {
        var customer = _customerRepository.GetCustomerByEmail(loginDTO.email);

        if (customer is Customer && _authService.validateCreditials(customer, loginDTO.password))
        {
            var token = _authService.generateToken(customer.email, customer.role.ToString());
            return Ok(new LoginResponseDTO(_mapper.Map<LoginUserDTO>(customer), token));
        }

        return Unauthorized();
    }
}