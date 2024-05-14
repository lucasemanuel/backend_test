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
}