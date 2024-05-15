using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories;

namespace CustomerApp.Repositories;

class CustomerRepository : ICustomerRepository
{
    private readonly IRepositoryBase<Customer> _repositoryBase;
    public CustomerRepository(IRepositoryBase<Customer> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public void Insert(Customer entity)
    {
        _repositoryBase.Insert(entity);
    }

    public Customer? GetCustomerById(string id)
    {
        return _repositoryBase.GetById(id);
    }

    public Customer? GetCustomerByEmail(string email)
    {
        Expression<Func<Customer, bool>> predicate = customer => customer.email == email;

        return _repositoryBase.GetByCriteria(predicate).FirstOrDefault();
    }
}