using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Modules.Volunteers.Contracts;
using SaveAnAnimal.Models;
using SaveAnAnimal.Repositories;

namespace SaveAnAnimal.Modules.Volunteers;

public class VolunteersModule : IModule
{
	public void RegisterModule(IServiceCollection services)
	{
		services.AddScoped<VolunteerRepository>();
	}

	public void MapEndpoints(IEndpointRouteBuilder app)
	{
		app.MapGet("/volunteer/{id}", GetVolunteer)
			.WithName(nameof(GetVolunteer))
			.Produces<VolunteerDetailsResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound);

		app.MapPost("/volunteer", PostVolunteer)
			.WithName(nameof(PostVolunteer))
			.Accepts<VolunteerDetailsRequest>(isOptional: false, MediaTypeNames.Application.Json)
			.Produces<VolunteerDetailsResponse>(StatusCodes.Status201Created)
			.Produces(StatusCodes.Status400BadRequest);

		app.MapGet("/volunteers", GetVolunteers)
			.WithName(nameof(GetVolunteers))
			.Produces<IEnumerable<VolunteerDetailsResponse>>(StatusCodes.Status200OK);
	}

	private async Task<IResult> GetVolunteer(string id, ILoggerFactory loggerFactory, VolunteerRepository repository)
	{
		var logger = loggerFactory.CreateLogger(nameof(GetVolunteer));
		bool parsed = UrlGuid.TryFromUrlString(id, out Guid guid);
		if (!parsed)
		{
			return Results.BadRequest($"Invalid volunteer id: {id}");
		}
		logger.LogInformation("Get Volunteer id '{Id}' request received", guid);
		var volunteer = await repository.GetVolunteerAsync(guid);
		if (volunteer is null)
		{
			return Results.NotFound($"Volunteer id '{id}' (guid '{guid}') not found");
		}
		VolunteerDetailsResponse response = new()
		{
			Id = volunteer.Id.ToUrlString(),
			FirstName = volunteer.FirstName,
			MiddleName = volunteer.MiddleName,
			LastName = volunteer.LastName,
			Email = volunteer.Email,
			PhoneNumber = volunteer.PhoneNumber,
			Address = volunteer.Address,
			City = volunteer.City
		};
		return Results.Ok(response);
	}

	private async Task<IResult> PostVolunteer(VolunteerDetailsRequest request, ILoggerFactory loggerFactory, VolunteerRepository repository)
	{
		var logger = loggerFactory.CreateLogger(nameof(PostVolunteer));
		logger.LogInformation("Post Volunteer request received");
		if (request is null)
		{
			return Results.BadRequest("No volunteer data provided");
		}
		Volunteer volunteer = new()
		{
			FirstName = request.FirstName,
			MiddleName = request.MiddleName,
			LastName = request.LastName,
			Email = request.Email,
			PhoneNumber = request.PhoneNumber,
			Address = request.Address,
			City = request.City
		};
		await repository.InsertVolunteerAsync(volunteer);
		VolunteerDetailsResponse response = new()
		{
			Id = volunteer.Id.ToUrlString(),
			FirstName = volunteer.FirstName,
			MiddleName = volunteer.MiddleName,
			LastName = volunteer.LastName,
			Email = volunteer.Email,
			PhoneNumber = volunteer.PhoneNumber,
			Address = volunteer.Address,
			City = volunteer.City
		};
		return Results.Created($"/volunteer/{response.Id}", response);
	}

	private async Task<IResult> GetVolunteers(ILoggerFactory loggerFactory, VolunteerRepository repository)
	{
		var logger = loggerFactory.CreateLogger(nameof(GetVolunteers));
		var volunteers = repository.GetVolunteers();
		var result = await volunteers.Select(v => new VolunteerDetailsResponse()
		{
			Id = v.Id.ToUrlString(),
			FirstName = v.FirstName,
			MiddleName = v.MiddleName,
			LastName = v.LastName,
			Email = v.Email,
			PhoneNumber = v.PhoneNumber,
			Address = v.Address,
			City = v.City
		}).ToListAsync();
		return Results.Ok(result);
	}
}
