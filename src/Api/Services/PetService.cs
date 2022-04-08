using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Repositories;

namespace SaveAnAnimal.Api.Services;

public interface IPetService
{
	IQueryable<Pet> AllPets();
	Task CreatePet(Pet pet);
	Task DeletePet(Guid petId);
	Task DeletePet(string petId);
	Task<PetCare?> GetCurrentCare(Pet pet);
	Task<Volunteer?> GetCurrentCaretaker(Pet pet);
	Task<Pet?> GetPetById(Guid guid);
	Task<Pet?> GetPetById(string id);
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

	public async Task<Pet?> GetPetById(string id)
	{
		bool parsed = UrlGuid.TryFromUrlString(id, out Guid guid);
		if (!parsed)
		{
			throw new FormatException("Invalid id.");
		}
		return await GetPetById(guid);
	}

	public async Task CreatePet(Pet pet)
	{
		_petRepository.Add(pet);
		await _petRepository.SaveAsync();
	}

	public Task<PetCare?> GetCurrentCare(Pet pet)
	{
		ArgumentNullException.ThrowIfNull(nameof(pet));

		var care = _petCareRepository.GetAll()
			.Where(x => x.Pet == pet)
			.Where(x => x.To == null)
			.OrderByDescending(x => x.From)
			.FirstOrDefault();
		return Task.FromResult(care);
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

	public async Task DeletePet(Guid petId)
	{
		_petRepository.Delete(petId);
		await _petRepository.SaveAsync();
	}

	public async Task DeletePet(string petId)
	{
		bool parsed = UrlGuid.TryFromUrlString(petId, out Guid guid);
		if (!parsed)
		{
			throw new FormatException("Invalid id.");
		}
		await DeletePet(guid);
	}
}

