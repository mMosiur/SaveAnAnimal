using Microsoft.EntityFrameworkCore;
using SaveAnAnimal.Api.Models;
using SaveAnAnimal.Api.Repositories;

namespace SaveAnAnimal.Api.Services;

public interface IVolunteerService
{
	IQueryable<Volunteer> AllVolunteers();
	Task<PetCare> AssignPet(Volunteer volunteer, Pet pet);
	Task CreateVolunteer(Volunteer volunteer);
	Task<bool> DeleteVolunteer(Guid volunteerId);
	IQueryable<PetCare> GetCurrentCares(Volunteer volunteer);
	Task<Volunteer?> GetVolunteerById(Guid volunteerId);
	Task UpdateVolunteer(Volunteer volunteer);
}

public class VolunteerService : IVolunteerService
{
	private readonly IVolunteerRepository _volunteerRepository;
	private readonly IPetCareRepository _petCareRepository;

	public VolunteerService(IVolunteerRepository volunteerRepository, IPetCareRepository petCareRepository)
	{
		_volunteerRepository = volunteerRepository;
		_petCareRepository = petCareRepository;
	}

	public IQueryable<Volunteer> AllVolunteers()
	{
		return _volunteerRepository.GetAll();
	}

	public async Task<Volunteer?> GetVolunteerById(Guid volunteerId)
	{
		return await _volunteerRepository.GetAsync(volunteerId);
	}

	public async Task CreateVolunteer(Volunteer volunteer)
	{
		_volunteerRepository.Add(volunteer);
		await _volunteerRepository.SaveAsync();
	}

	public IQueryable<PetCare> GetCurrentCares(Volunteer volunteer)
	{
		return _petCareRepository.GetAll()
			.Where(x => x.Caretaker == volunteer)
			.Where(x => x.To == null);
	}

	public async Task<PetCare> AssignPet(Volunteer volunteer, Pet pet)
	{
		var care = await _petCareRepository.GetAll()
			.Where(pc => pc.Pet == pet)
			.Where(pc => pc.To == null)
			.FirstOrDefaultAsync();
		if (care != null)
		{
			care.To = DateTime.Now;
		}
		care = new PetCare()
		{
			Pet = pet,
			Caretaker = volunteer,
			From = DateTime.Now,
			To = null
		};
		_petCareRepository.Add(care);
		await _petCareRepository.SaveAsync();
		return care;
	}

	public async Task UpdateVolunteer(Volunteer volunteer)
	{
		ArgumentNullException.ThrowIfNull(nameof(volunteer));

		_volunteerRepository.Update(volunteer);
		await _volunteerRepository.SaveAsync();
	}

	public async Task<bool> DeleteVolunteer(Guid volunteerId)
	{
		bool deleted = _volunteerRepository.Delete(volunteerId);
		await _volunteerRepository.SaveAsync();
		return deleted;
	}
}
