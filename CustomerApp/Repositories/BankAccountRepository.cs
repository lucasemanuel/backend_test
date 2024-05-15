using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Repositories;

class BankAccountRepository : IBankAccountRepository
{
    private readonly IRepositoryBase<BankAccount> _repositoryBase;
    public BankAccountRepository(IRepositoryBase<BankAccount> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public IEnumerable<BankAccount> GetByAll()
    {
        Expression<Func<BankAccount, bool>> predicate = bankAccount => bankAccount._id != null;

        return _repositoryBase.GetByCriteria(predicate).ToList();
    }

    public BankAccount? GetByNumberAccount(string accountNumber)
    {
        Expression<Func<BankAccount, bool>> predicate = bankAccount => bankAccount.accountNumber == accountNumber;

        return _repositoryBase.GetByCriteria(predicate).FirstOrDefault();
    }
    
    public BankAccount? GetByCustomer(Customer customer)
    {
        Expression<Func<BankAccount, bool>> predicate = bankAccount => bankAccount.Customer_id.ToString() == customer._id.ToString();

        return _repositoryBase.GetByCriteria(predicate).FirstOrDefault();
    }

    public void Insert(BankAccount entity)
    {
        _repositoryBase.Insert(entity);
    }

    public void Update(BankAccount entity)
    {
        _repositoryBase.Update(entity);
    }

    public void Delete(BankAccount entity)
    {
        _repositoryBase.Delete(entity);
    }
}