using CustomerApp.Entities;

namespace CustomerApp.Repositories;

public interface ICustomerRepository
{
    void Insert(Customer entity);
}