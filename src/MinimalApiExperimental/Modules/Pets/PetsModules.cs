using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Models;
using SaveAnAnimal.Modules.Pets.Contracts;
using SaveAnAnimal.Repositories;

namespace SaveAnAnimal.Modules.Pets;

public class PetsModule : IModule
{
	public void RegisterModule(IServiceCollection services)
	{
		services.AddScoped<PetRepository>();
	}

	public void MapEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet("/pet/{id}", GetPet)
			.Produces<PetDetailsResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound)
			.WithName(nameof(GetPet));

		app.MapPost("/pet", PostPet)
			.Accepts<PetDetailsRequest>(isOptional: false, MediaTypeNames.Application.Json)
			.Produces<PetDetailsResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName(nameof(PostPet));

		app.MapGet("/pets", GetPets)
			.Produces<IEnumerable<PetDetailsResponse>>(StatusCodes.Status200OK)
			.WithName(nameof(GetPets));
	}

	private async Task<IResult> GetPet(string id, ILoggerFactory loggerFactory, PetRepository repository)
	{
		var logger = loggerFactory.CreateLogger(nameof(GetPet));
		bool parsed = UrlGuid.TryFromUrlString(id, out Guid guid);
		if (!parsed)
		{
			return Results.BadRequest($"Invalid pet id: {id}");
		}
		logger.LogInformation("Get pet id '{Id}' request received", guid);
		var pet = await repository.GetPetAsync(guid);
		if (pet is null)
		{
			return Results.NotFound($"Pet id '{id}' (guid '{guid}') not found");
		}
		return Results.Ok(pet);
	}

	private async Task<IResult> PostPet(Pet pet, ILoggerFactory loggerFactory, PetRepository repository)
	{
		var logger = loggerFactory.CreateLogger(nameof(PostPet));
		logger.LogInformation("Post Pet request received");
		if (pet is null)
		{
			return Results.BadRequest("No pet data provided");
		}
		if (pet.Id != Guid.Empty)
		{
			return Results.BadRequest("Pet id must be empty");
		}
		await repository.InsertPetAsync(pet);
		return Results.Created($"/pet/{pet.Id.ToUrlString()}", pet);
	}

	public async Task<IResult> GetPets(ILoggerFactory loggerFactory, PetRepository repository)
	{
		var logger = loggerFactory.CreateLogger(nameof(GetPets));
		var pets = repository.GetPets();
		var result = await pets.Select(p => new PetDetailsResponse(
			p.Id.ToUrlString(),
			p.Name,
			p.Type,
			p.Color
		)).ToListAsync();
		return Results.Ok(result);
	}
}
