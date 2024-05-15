using Domain.Entities;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Customer? GetCustomerById(string id);
    Customer? GetCustomerByEmail(string email);
    void Insert(Customer entity);
}