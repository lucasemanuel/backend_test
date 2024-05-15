using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories;

namespace CustomerApp.Repositories;

class BankTransactionRepository : IBankTransactionRepository
{
    private readonly IRepositoryBase<BankTransaction> _repositoryBase;
    public BankTransactionRepository(IRepositoryBase<BankTransaction> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public IEnumerable<BankTransaction> GetAllByBankAccount(BankAccount entity)
    {
        Expression<Func<BankTransaction, bool>> predicate = bannTransaction => bannTransaction.BankAccount_id.ToString() == entity._id.ToString();

        return _repositoryBase.GetByCriteria(predicate).ToList();
    }

    public IEnumerable<BankTransaction> GetAllByBankAccountPerPeriod(BankAccount entity, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public void Insert(BankTransaction entity)
    {
        _repositoryBase.Insert(entity);
    }
}