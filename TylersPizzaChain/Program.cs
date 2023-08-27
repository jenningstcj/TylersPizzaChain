using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Pipelines;

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
builder.Services.AddTransient<ImpureDependencies>();

builder.Services.AddHttpClient("DoorDashClient", _ => _.BaseAddress = new Uri("http://localhost:8888"));
builder.Services.AddHttpClient("GrubHubClient", _ => _.BaseAddress = new Uri("http://localhost:8887"));
builder.Services.AddHttpClient("UberEatsClient", _ => _.BaseAddress = new Uri("http://localhost:8886"));
builder.Services.AddHttpClient("PointOfSaleClient", _ => _.BaseAddress = new Uri("http://localhost:8885"));
builder.Services.AddHttpClient("PaymentProcessorClient", _ => _.BaseAddress = new Uri("http://localhost:8884"));

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
