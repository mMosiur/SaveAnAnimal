using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SaveAnAnimal.Modules;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
	{
		options.SwaggerDoc("v1", new OpenApiInfo { Title = "SaveAnAnimal API", Version = "v1" });
		options.UseInlineDefinitionsForEnums();
	}
);

// Configure logging
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<AppDbContext>(
	options =>
	{
		options.UseSqlite("Data Source=app.db");
	}
);

// Adds services from all classes that implement IModule interface.
// Based on its RegisterModule method.
builder.Services.AddModules();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Maps all endpoints defined in classes that implement IModule interface.
// Based on its MapEndpoints method.
app.MapEndpoints();

app.MapGet("/encode/{id:guid}", (Guid id) =>
{
	string encoded = UrlGuid.ToUrlString(id);
	return Results.Ok(encoded);
}).WithName("Encode");

app.Run();
