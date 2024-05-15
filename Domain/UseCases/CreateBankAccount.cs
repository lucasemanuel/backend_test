using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Domain.UseCases;

public class CreateBankAccount
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IGenerateNumberAccount _generateNumberAccount;

    public CreateBankAccount(IBankAccountRepository bankAccountRepository, IGenerateNumberAccount generateNumberAccount)
    {
        _bankAccountRepository = bankAccountRepository;
        _generateNumberAccount = generateNumberAccount;
    }

    public BankAccount Handle(Customer customer)
    {
        if (_bankAccountRepository.GetByCustomer(customer) != null)
        {
            throw new DomainException("Customer already has account.");
        }

        string accountNumber = _generateNumberAccount.Generate();
        BankAccount bankAccount = new BankAccount();
        bankAccount.accountNumber = accountNumber;
        bankAccount.Customer = customer;
        bankAccount.balance = 0;
        bankAccount.isInactived = false;
        
        _bankAccountRepository.Insert(bankAccount);

        return bankAccount;
    }
}