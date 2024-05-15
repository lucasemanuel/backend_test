using Domain.Entities;
using Domain.Repositories;

namespace Domain.UseCases;

public class Deposit
{
    private readonly IBankTransactionRepository _bankTransactionRepository;
    private readonly IBankAccountRepository _bankAccountRepository;

    public Deposit(IBankTransactionRepository bankTransactionRepository, IBankAccountRepository bankAccountRepository)
    {
        _bankTransactionRepository = bankTransactionRepository;
        _bankAccountRepository = bankAccountRepository;
    }

    public void Handle(BankAccount bankAccount, decimal amount)
    {
        BankTransaction bankTransaction = new BankTransaction().Deposit(bankAccount, amount);
        _bankTransactionRepository.Insert(bankTransaction);
        _bankAccountRepository.Update(bankTransaction.BankAccount);
    }
}