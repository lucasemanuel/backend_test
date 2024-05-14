using Domain.Entities;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    void Insert(Customer entity);
}