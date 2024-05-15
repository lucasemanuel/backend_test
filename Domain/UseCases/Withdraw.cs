using Domain.Entities;
using Domain.Repositories;

namespace Domain.UseCases;

public class Withdraw
{
    private readonly IBankTransactionRepository _bankTransactionRepository;
    private readonly IBankAccountRepository _bankAccountRepository;

    public Withdraw(IBankTransactionRepository bankTransactionRepository, IBankAccountRepository bankAccountRepository)
    {
        _bankTransactionRepository = bankTransactionRepository;
        _bankAccountRepository = bankAccountRepository;
    }

    public void Handle(BankAccount bankAccount, decimal amount)
    {
        BankTransaction bankTransaction = new BankTransaction().Withdraw(bankAccount, amount);
        _bankTransactionRepository.Insert(bankTransaction);
        _bankAccountRepository.Update(bankTransaction.BankAccount);
    }
}