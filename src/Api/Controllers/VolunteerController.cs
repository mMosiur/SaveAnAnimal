using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Contracts;
using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Services;

namespace SaveAnAnimal.Api.Controllers;

[ApiController]
[Route("volunteer")]
public class VolunteerController : ControllerBase
{
	readonly ILogger<VolunteerController> logger;
	readonly IVolunteerService volunteerService;

	public VolunteerController(ILogger<VolunteerController> logger, IVolunteerService volunteerService)
	{
		this.logger = logger;
		this.volunteerService = volunteerService;
	}

	[HttpGet]
	public async Task<IActionResult> GetVolunteers()
	{
		var volunteers = volunteerService.AllVolunteers();
		var volunteersList = await volunteers
			.Select(v => VolunteerDetailsResponse.BuildFrom(v))
			.ToListAsync();
		return Ok(volunteersList);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetVolunteer(string id)
	{
		bool parsed = UrlGuid.TryFromUrlString(id, out Guid guid);
		if (!parsed)
		{
			return BadRequest($"Invalid volunteer id: {id}");
		}
		logger.LogInformation("Get Volunteer id '{guid}' request received", guid);
		var volunteer = await volunteerService.GetVolunteerById(guid);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' (guid '{guid}') not found");
		}
		return Ok(VolunteerDetailsResponse.BuildFrom(volunteer));
	}

	[HttpPost]
	public async Task<IActionResult> PostVolunteer(VolunteerDetailsRequest request)
	{
		logger.LogInformation("Post Volunteer request received");
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
		await volunteerService.CreateVolunteer(volunteer);
		var response = VolunteerDetailsResponse.BuildFrom(volunteer);
		return CreatedAtAction(nameof(GetVolunteer), new { id = response.Id }, response);

	}

	[HttpGet("{id}/pets")]
	public async Task<IActionResult> GetVolunteerAssignedPets(string id)
	{
		bool parsed = UrlGuid.TryFromUrlString(id, out Guid volunteerGuid);
		if (!parsed)
		{
			return BadRequest($"Invalid volunteer id: {id}");
		}
		logger.LogInformation("Get Volunteer id '{guid}' request received", volunteerGuid);
		var volunteer = await volunteerService.GetVolunteerById(volunteerGuid);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' (guid '{volunteerGuid}') not found");
		}
		var result = volunteerService.GetCurrentCares(volunteer).Select(pc => pc.Pet);
		return Ok(result);
	}

	[HttpPost("{id}/assign-pet")]
	public async Task<IActionResult> PostVolunteerAssignPet(string id, AssignPetRequest request, [FromServices] PetService petService)
	{
		bool parsed = UrlGuid.TryFromUrlString(id, out Guid volunteerGuid);
		if (!parsed)
		{
			return BadRequest($"Invalid volunteer id: {id}");
		}
		logger.LogInformation("Get Volunteer id '{guid}' request received", volunteerGuid);
		var volunteer = await volunteerService.GetVolunteerById(volunteerGuid);
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' (guid '{volunteerGuid}') not found");
		}
		parsed = UrlGuid.TryFromUrlString(request.PetId, out Guid petGuid);
		if (!parsed)
		{
			return BadRequest($"Invalid pet id: {request.PetId}");
		}
		logger.LogInformation("Post Volunteer Assign Pet request received");
		var pet = await petService.GetPetById(petGuid);
		if (pet is null)
		{
			return NotFound($"Pet id '{request.PetId}' (guid '{petGuid}') not found");
		}
		await volunteerService.AssignPet(volunteer, pet);
		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateVolunteer(string id, VolunteerDetailsRequest request)
	{
		logger.LogInformation("Put Volunteer request received");
		Volunteer? volunteer;
		try
		{
			volunteer = await volunteerService.GetVolunteerById(id);
		}
		catch (FormatException)
		{
			return BadRequest($"Invalid volunteer id: '{id}'");
		}
		if (volunteer is null)
		{
			return NotFound($"Volunteer id '{id}' not found");
		}
		volunteer.FirstName = request.FirstName;
		volunteer.MiddleName = request.MiddleName;
		volunteer.LastName = request.LastName;
		volunteer.Email = request.Email;
		volunteer.PhoneNumber = request.PhoneNumber;
		volunteer.Address = request.Address;
		volunteer.City = request.City;
		await volunteerService.UpdateVolunteer(volunteer);
		return Ok(VolunteerDetailsResponse.BuildFrom(volunteer));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteVolunteer(string id)
	{
		logger.LogInformation("Delete Volunteer request received");
		try
		{
			await volunteerService.DeleteVolunteer(id);
			return Ok();
		}
		catch (FormatException)
		{
			return BadRequest($"Invalid volunteer id: '{id}'");
		}
	}
}
