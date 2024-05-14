using Infra.Database;
using CustomerApp.Mappers;
using CustomerApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<MongoDbContext>(options => options.UseMongoDB(Environment.GetEnvironmentVariable("MONGODB_URI") ?? "", Environment.GetEnvironmentVariable("MONGODB_DATABASE") ?? ""));

// Repositories
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Controllers and Routes
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(DTOMappers));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
