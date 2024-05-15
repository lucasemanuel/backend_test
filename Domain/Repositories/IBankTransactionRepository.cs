using Domain.Entities;

namespace Domain.Repositories;

public interface IBankTransactionRepository
{
    IEnumerable<BankTransaction> GetAllByBankAccount(BankAccount entity);
    IEnumerable<BankTransaction> GetAllByBankAccountPerPeriod(BankAccount entity, DateTime startDate, DateTime endDate);
    void Insert(BankTransaction entity);
}