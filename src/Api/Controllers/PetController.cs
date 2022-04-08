using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Contracts;
using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Repositories;
using SaveAnAnimal.Api.Services;

namespace SaveAnAnimal.Api.Controllers;

[ApiController]
[Route("pet")]
public class PetController : ControllerBase
{
	private readonly ILogger<PetController> logger;
	private readonly IPetService petService;

	public PetController(ILogger<PetController> logger, IPetService petService)
	{
		this.logger = logger;
		this.petService = petService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPets()
	{
		var pets = petService.AllPets();
		var petList = await pets
			.Select(p => PetDetailsResponse.BuildFrom(p))
			.ToListAsync();
		return Ok(petList);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetPet(string id)
	{
		Pet? pet;
		try
		{
			pet = await petService.GetPetById(id);
		}
		catch(FormatException)
		{
			return BadRequest($"Invalid pet id: '{id}'");
		}
		if (pet is null)
		{
			return NotFound($"Pet id '{id}' not found");
		}
		return Ok(PetDetailsResponse.BuildFrom(pet));
	}

	[HttpGet("{id}/current-care")]
	public async Task<IActionResult> GetPetCurrentCare(string id)
	{
		Pet? pet;
		try
		{
			pet = await petService.GetPetById(id);
		}
		catch (FormatException)
		{
			return BadRequest($"Invalid pet id: '{id}'");
		}
		if (pet is null)
		{
			return NotFound($"Pet id '{id}' not found");
		}
		var care = await petService.GetCurrentCare(pet);
		if (care is null || care.To is not null)
		{
			return NotFound($"Pet id '{id}' is not currently being cared for");
		}
		return Ok(PetCareDetailsResponse.BuildFrom(care));
	}

	[HttpPost]
	public async Task<IActionResult> CreatePet(PetDetailsRequest request)
	{
		logger.LogInformation("Post Pet request received");
		var pet = new Pet()
		{
			Name = request.Name,
			Type = request.Type,
			Color = request.Color
		};
		await petService.CreatePet(pet);
		var response = PetDetailsResponse.BuildFrom(pet);
		return Created($"/pet/{response.Id}", response);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdatePet(string id, PetDetailsRequest request)
	{
		logger.LogInformation("Put Pet request received");
		Pet? pet;
		try
		{
			pet = await petService.GetPetById(id);
		}
		catch (FormatException)
		{
			return BadRequest($"Invalid pet id: '{id}'");
		}
		if (pet is null)
		{
			return NotFound($"Pet id '{id}' not found");
		}
		pet.Name = request.Name;
		pet.Type = request.Type;
		pet.Color = request.Color;
		await petService.UpdatePet(pet);
		return Ok(PetDetailsResponse.BuildFrom(pet));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletePet(string id)
	{
		logger.LogInformation("Delete Pet request received");
		try
		{
			await petService.DeletePet(id);
			return Ok();
		}
		catch (FormatException)
		{
			return BadRequest($"Invalid volunteer id: '{id}'");
		}
	}
}
