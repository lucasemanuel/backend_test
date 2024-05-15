using Domain.Entities;

namespace Domain.Repositories;

public interface IBankAccountRepository
{
    IEnumerable<BankAccount> GetByAll();
    BankAccount? GetByCustomer(Customer customer);
    BankAccount? GetByNumberAccount(string accountNumber);
    void Insert(BankAccount entity);
    void Update(BankAccount entity);
    void Delete(BankAccount entity);
}