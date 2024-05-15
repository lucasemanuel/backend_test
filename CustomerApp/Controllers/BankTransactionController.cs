using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.Controllers;

[ApiController]
[Route("api/transaction")]
public class BankTransactionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBankTransactionRepository _bankTransactionRepository;
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankTransactionController(
        IBankTransactionRepository bankTransactionRepository,
        IBankAccountRepository bankAccountRepository,
        IMapper mapper
    )
    {
        _bankTransactionRepository = bankTransactionRepository;
        _bankAccountRepository = bankAccountRepository;
        _mapper = mapper;
    }

    [HttpGet("{number}")]
    [Authorize]
    public IActionResult listTransactionsByAccount(string number)
    {
        var bankAccount = _bankAccountRepository.GetByNumberAccount(number);
        if (bankAccount != null) {
            return Ok(_bankTransactionRepository.GetAllByBankAccount(bankAccount).Select(c => _mapper.Map<TransactionResponseDTO>(c)).Reverse());
        }

        return Ok();
    }

    [HttpPost]
    [Authorize]
    public IActionResult transaction(TransactionRequestDTO transactionDTO)
    {
        var bankAccount = _bankAccountRepository.GetByNumberAccount(transactionDTO.bankAccountNumber);
        if (bankAccount == null)
        {
            return NotFound("Account not found");
        }

        try
        {
            if (transactionDTO.type == TypeBankTransaction.Withdraw)
            {
                var transaction = new BankTransaction().Withdraw(bankAccount, transactionDTO.amount);
                _bankTransactionRepository.Insert(transaction);
                _bankAccountRepository.Update(transaction.BankAccount);
                return Ok(_mapper.Map<TransactionResponseDTO>(transaction));
            }
            else if (transactionDTO.type == TypeBankTransaction.Deposit)
            {
                var transaction = new BankTransaction().Deposit(bankAccount, transactionDTO.amount);
                _bankTransactionRepository.Insert(transaction);
                _bankAccountRepository.Update(transaction.BankAccount);
                return Ok(_mapper.Map<TransactionResponseDTO>(transaction));
            }
            else {
                return BadRequest("Operation not implemented.");
            }
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
    }
}