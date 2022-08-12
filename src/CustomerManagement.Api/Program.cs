using CustomerManagement.Domain.Features.Customers;
using CustomerManagement.Infrastructure.Configuration;
using MediatR;
using System.Reflection;
using System.Runtime.CompilerServices;
using Serilog;
using CustomerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("CustomerManagement.IntegrationTest")] 

var builder = WebApplication.CreateBuilder(args);

//Logging
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.WebHost.UseSerilog();

//Services
builder.Services.ConfigureData(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serviceProvider = builder.Services.BuildServiceProvider();
var context = serviceProvider.GetService<CustomerManagementDbContext>();

//App
var app = builder.Build();

try
{
    context.Database.Migrate();
}
catch (Exception)
{
}

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();
public partial class Program { }