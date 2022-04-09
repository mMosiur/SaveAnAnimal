using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Contracts.Responses;
using SaveAnAnimal.Api.Contracts.Requests;
using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Services;
using AutoMapper;

namespace SaveAnAnimal.Api.Controllers;

[ApiController]
[Route("volunteer")]
public class VolunteerController : ControllerBase
{
	readonly ILogger<VolunteerController> _logger;
	readonly IVolunteerService _volunteerService;
	readonly IMapper _mapper;

	public VolunteerController(ILogger<VolunteerController> logger, IVolunteerService volunteerService, IMapper mapper)
	{
		_logger = logger;
		_volunteerService = volunteerService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetVolunteers()
	{
		var volunteers = _volunteerService.AllVolunteers();

		var volunteersList = await volunteers
			.Select(v => _mapper.Map<VolunteerDetailsResponse>(v))
			.ToListAsync();
		return Ok(volunteersList);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetVolunteer(Guid id)
	{
		_logger.LogInformation("Get Volunteer id '{id}' request received", id);
		var volunteer = await _volunteerService.GetVolunteerById(id);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' not found");
		}
		var response = _mapper.Map<VolunteerDetailsResponse>(volunteer);
		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> PostVolunteer(VolunteerDetailsRequest request)
	{
		_logger.LogInformation("Post Volunteer request received");
		if (request is null)
		{
			return BadRequest("No volunteer data provided");
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
		await _volunteerService.CreateVolunteer(volunteer);
		var response = _mapper.Map<VolunteerDetailsResponse>(volunteer);
		return CreatedAtAction(nameof(GetVolunteer), new { id = response.Id }, response);
	}

	[HttpGet("{id:guid}/pets")]
	public async Task<IActionResult> GetVolunteerAssignedPets(Guid id)
	{
		_logger.LogInformation("Get Volunteer id '{id}' request received", id);
		var volunteer = await _volunteerService.GetVolunteerById(id);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' not found");
		}
		var result = _volunteerService.GetCurrentCares(volunteer).Select(pc => pc.Pet);
		return Ok(result);
	}

	[HttpPost("{id:guid}/assign-pet")]
	public async Task<IActionResult> PostVolunteerAssignPet(Guid id, AssignPetRequest request, [FromServices] PetService petService)
	{
		_logger.LogInformation("Get Volunteer id '{id}' request received", id);
		var volunteer = await _volunteerService.GetVolunteerById(id);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' not found");
		}
		var pet = await petService.GetPetById(request.PetId);
		if (pet is null)
		{
			return NotFound($"Pet id '{request.PetId}' not found");
		}
		await _volunteerService.AssignPet(volunteer, pet);
		return Ok();
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateVolunteer(Guid id, UpdateVolunteerDetailsRequest request)
	{
		_logger.LogInformation("Put Volunteer request received");
		var volunteer = await _volunteerService.GetVolunteerById(id);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' not found");
		}
		volunteer.FirstName = request.FirstName ?? volunteer.FirstName;
		volunteer.MiddleName = request.MiddleName ?? volunteer.MiddleName;
		volunteer.LastName = request.LastName ?? volunteer.LastName;
		volunteer.Email = request.Email ?? volunteer.Email;
		volunteer.PhoneNumber = request.PhoneNumber ?? volunteer.PhoneNumber;
		volunteer.Address = request.Address ?? volunteer.Address;
		volunteer.City = request.City ?? volunteer.City;
		await _volunteerService.UpdateVolunteer(volunteer);
		var response = _mapper.Map<VolunteerDetailsResponse>(volunteer);
		return Ok(response);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteVolunteer(Guid id)
	{
		_logger.LogInformation("Delete Volunteer request received");
		bool deleted = await _volunteerService.DeleteVolunteer(id);
		return deleted ? Ok() : NotFound($"Volunteer id '{id}' not found");
	}
}
