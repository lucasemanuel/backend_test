using CustomerApp.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CustomerApp.Data;
public class MongoDbContext : DbContext
{
    public DbSet<Customer> Customer { get; set; }

    public static MongoDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<MongoDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public MongoDbContext(DbContextOptions<MongoDbContext> options)
        : base(options)
    {
    }
}