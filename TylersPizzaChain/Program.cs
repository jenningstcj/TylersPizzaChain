using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Clients;
using TylersPizzaChain.Database;
using TylersPizzaChain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(_ =>
{
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
    foreach (var xmlFile in xmlFiles)
    {
        _.IncludeXmlComments(xmlFile);
    }
    _.UseInlineDefinitionsForEnums();
});

builder.Services.AddDbContext<TylersPizzaDbContext>(
        options => options.UseInMemoryDatabase("tylerPizzaChain"));

//DI
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddTransient<IDeliveryCoordinatorService, DeliveryCoordinatorService>();
builder.Services.AddTransient<IStoreHoursValidationService, StoreHoursValidationService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IPaymentProcessorClient, PaymentProcessorClient>();
builder.Services.AddTransient<IPointOfSaleClient, PointOfSaleClient>();
builder.Services.AddTransient<IDoorDashClient, DoorDashClient>();
builder.Services.AddTransient<IGrubHubClient, GrubHubClient>();
builder.Services.AddTransient<IUberEatsClient, UberEatsClient>();

builder.Services.AddHttpClient<IDoorDashClient, DoorDashClient>(_ => _.BaseAddress = new Uri("http://localhost:8888"));
builder.Services.AddHttpClient<IGrubHubClient, GrubHubClient>(_ => _.BaseAddress = new Uri("http://localhost:8887"));
builder.Services.AddHttpClient<IUberEatsClient, UberEatsClient>(_ => _.BaseAddress = new Uri("http://localhost:8886"));
builder.Services.AddHttpClient<IPointOfSaleClient, PointOfSaleClient>(_ => _.BaseAddress = new Uri("http://localhost:8885"));
builder.Services.AddHttpClient<IPaymentProcessorClient, PaymentProcessorClient>(_ => _.BaseAddress = new Uri("http://localhost:8884"));

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

if (app.Environment.IsDevelopment())
{
    app.SeedDatabase(); //seed test data
}

app.Run();
