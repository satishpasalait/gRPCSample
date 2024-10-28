using Microsoft.EntityFrameworkCore;
using gRPC.Infrastructure.Data;
using gRPC.Service.Services;
using gRPC.Domain.Repositories;
using gRPC.Infrastructure.Persistance;
using gRPC.Service.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=Database/Shopping.db"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(ServiceProfile).Assembly);

var app = builder.Build();

app.MapGrpcService<ProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
