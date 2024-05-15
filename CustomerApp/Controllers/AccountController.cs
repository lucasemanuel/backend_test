using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly CreateBankAccount _createBankAccount;
    private readonly CloseBankAccount _closeBankAccount;

    public AccountController(
        IMapper mapper,
        ICustomerRepository customerRepository,
        IBankAccountRepository bankAccountRepository,
        CreateBankAccount createBankAccount,
        CloseBankAccount closeBankAccount
    )
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _bankAccountRepository = bankAccountRepository;
        _createBankAccount = createBankAccount;
        _closeBankAccount = closeBankAccount;
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public IActionResult listAccounts()
    {
        return Ok(_bankAccountRepository.GetByAll());
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public IActionResult CreateAccount(CreateAccountRequestDTO accountRequestDTO)
    {
        var customer = _customerRepository.GetCustomerByEmail(accountRequestDTO.email);

        try
        {
            _createBankAccount.Handle(customer);
            return Ok();
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{number}")]
    [Authorize(Policy = "Admin")]
    public IActionResult deleteAccount(string number)
    {
        var bankAccount = _bankAccountRepository.GetByNumberAccount(number);
        if (bankAccount == null)
        {
            return NotFound("Account not found");
        }

        _closeBankAccount.Handle(bankAccount);
        return NoContent();
    }
}