using Domain.Entities;
using Domain.Repositories;

namespace Domain.UseCases;

public class CloseBankAccount
{
    private readonly IBankTransactionRepository _bankTransactionRepository;
    private readonly IBankAccountRepository _bankAccountRepository;

    public CloseBankAccount(IBankTransactionRepository bankTransactionRepository, IBankAccountRepository bankAccountRepository)
    {
        _bankTransactionRepository = bankTransactionRepository;
        _bankAccountRepository = bankAccountRepository;
    }

    public void Handle(BankAccount bankAccount)
    {
        if (_bankTransactionRepository.GetAllByBankAccount(bankAccount).Count() == 0)
        {
            _bankAccountRepository.Delete(bankAccount);
            return;
        }

        bankAccount.isInactived = true;
        _bankAccountRepository.Update(bankAccount);
    }
}