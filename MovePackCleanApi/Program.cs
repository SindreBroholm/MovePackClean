using DataSource;
using DataSource.Stores;
using MovePackCleanApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DapperContext>();
// Stores
builder.Services.AddScoped<IServiceTypesStore, ServiceTypeStore>();
builder.Services.AddScoped<ICustomerStore, CustomerStore>();
builder.Services.AddScoped<IOrderStore, OrderStore>();
builder.Services.AddScoped<IOrderDetailStore, OrderDetailStore>();
// Services
builder.Services.AddScoped<ServiceTypeService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
