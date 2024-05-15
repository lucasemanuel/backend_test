using Domain.Enums;
using MongoDB.Bson;

namespace Domain.Entities;

public class Customer
{
    public ObjectId _id { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public Role? role { get; set; }
}