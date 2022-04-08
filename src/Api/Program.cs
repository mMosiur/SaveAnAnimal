using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SaveAnAnimal.Api;
using SaveAnAnimal.Api.Repositories;
using SaveAnAnimal.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "SaveAnAnimal API", Version = "v1" });
	options.UseInlineDefinitionsForEnums();
});

// Configure Serilog logging
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlite("Data Source=app.db");
});

builder.Services.AddScoped<IVolunteerRepository, VolunteerRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetCareRepository, PetCareRepository>();
builder.Services.AddScoped<IVolunteerService, VolunteerService>();
builder.Services.AddScoped<IPetService, PetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors(policy =>
{
	policy.AllowAnyOrigin();
});

app.Run();
