using MongoDB.Bson;

namespace CustomerApp.Entities;

public class Customer
{
    public ObjectId _id { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
}