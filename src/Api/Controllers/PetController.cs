using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Contracts.Requests;
using SaveAnAnimal.Api.Contracts.Responses;
using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Services;

namespace SaveAnAnimal.Api.Controllers;

[ApiController]
[Route("pet")]
public class PetController : ControllerBase
{
	private readonly ILogger<PetController> _logger;
	private readonly IPetService _petService;
	private readonly IMapper _mapper;

	public PetController(ILogger<PetController> logger, IPetService petService, IMapper mapper)
	{
		_logger = logger;
		_petService = petService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetPets()
	{
		var pets = _petService.AllPets();
		var petList = await pets
			.Select(p => _mapper.Map<PetDetailsResponse>(p))
			.ToListAsync();
		return Ok(petList);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetPet(Guid id)
	{
		Pet? pet;
		try
		{
			pet = await _petService.GetPetById(id);
		}
		catch(FormatException)
		{
			return BadRequest($"Invalid pet id: '{id}'");
		}
		if (pet is null)
		{
			return NotFound($"Pet id '{id}' not found");
		}
		var response = _mapper.Map<PetDetailsResponse>(pet);
		return Ok(response);
	}

	[HttpGet("{id:guid}/current-care")]
	public async Task<IActionResult> GetPetCurrentCare(Guid id)
	{
		Pet? pet;
		try
		{
			pet = await _petService.GetPetById(id);
		}
		catch (FormatException)
		{
			return BadRequest($"Invalid pet id: '{id}'");
		}
		if (pet is null)
		{
			return NotFound($"Pet id '{id}' not found");
		}
		var care = await _petService.GetCurrentCare(pet);
		if (care is null || care.To is not null)
		{
			return NotFound($"Pet id '{id}' is not currently being cared for");
		}
		var response = _mapper.Map<PetCareDetailsResponse>(care);
		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> CreatePet(PetDetailsRequest request)
	{
		_logger.LogInformation("Post Pet request received");
		var pet = _mapper.Map<Pet>(request);
		await _petService.CreatePet(pet);
		var response = _mapper.Map<PetDetailsResponse>(pet);
		return Created($"/pet/{response.Id}", response);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdatePet(Guid id, UpdatePetDetailsRequest request)
	{
		_logger.LogInformation("Put Pet request received");
		var pet = await _petService.GetPetById(id);
		if (pet is null)
		{
			return NotFound($"Pet id '{id}' not found");
		}
		pet.Name = request.Name ?? pet.Name;
		pet.Type = request.Type ?? pet.Type;
		pet.Color = request.Color ?? pet.Color;
		await _petService.UpdatePet(pet);
		var response = _mapper.Map<PetDetailsResponse>(pet);
		return Ok(response);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeletePet(Guid id)
	{
		_logger.LogInformation("Delete Pet request received");
		bool deleted = await _petService.DeletePet(id);
		return deleted ? Ok() : NotFound($"Pet id '{id}' not found");
	}
}
