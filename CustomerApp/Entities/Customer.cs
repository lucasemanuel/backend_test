using MongoDB.Bson;

namespace CustomerApp.Entities;

public class Customer : Domain.Entities.Customer
{
    public Guid id { get; set; }
}