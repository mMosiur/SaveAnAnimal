using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Repositories;

namespace SaveAnAnimal.Api.Services;

public interface IPetService
{
	IQueryable<Pet> AllPets();
	Task CreatePet(Pet pet);
	Task<bool> DeletePet(Guid petId);
	Task<PetCare?> GetCurrentCare(Pet pet);
	Task<Volunteer?> GetCurrentCaretaker(Pet pet);
	Task<Pet?> GetPetById(Guid guid);
	Task UpdatePet(Pet pet);
}

public class PetService : IPetService
{
	private readonly IPetRepository _petRepository;
	private readonly IPetCareRepository _petCareRepository;

	public PetService(IPetRepository petRepository, IPetCareRepository petCareRepository)
	{
		_petRepository = petRepository;
		_petCareRepository = petCareRepository;
	}

	public IQueryable<Pet> AllPets()
	{
		return _petRepository.GetAll();
	}

	public async Task<Pet?> GetPetById(Guid guid)
	{
		return await _petRepository.GetAsync(guid);
	}

	public async Task CreatePet(Pet pet)
	{
		_petRepository.Add(pet);
		await _petRepository.SaveAsync();
	}

	public async Task<PetCare?> GetCurrentCare(Pet pet)
	{
		ArgumentNullException.ThrowIfNull(nameof(pet));

		var care = await _petCareRepository.GetAll()
			.Where(x => x.Pet == pet)
			.Where(x => x.To == null)
			.OrderByDescending(x => x.From)
			.FirstOrDefaultAsync();
		return care;
	}

	public async Task<Volunteer?> GetCurrentCaretaker(Pet pet)
	{
		var care = await GetCurrentCare(pet);
		return care?.Caretaker;
	}

	public async Task UpdatePet(Pet pet)
	{
		ArgumentNullException.ThrowIfNull(nameof(pet));

		_petRepository.Update(pet);
		await _petRepository.SaveAsync();
	}

	public async Task<bool> DeletePet(Guid petId)
	{
		bool deleted = _petRepository.Delete(petId);
		await _petRepository.SaveAsync();
		return deleted;
	}
}

