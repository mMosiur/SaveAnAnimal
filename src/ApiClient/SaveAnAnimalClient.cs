using AutoMapper;
using SaveAnAnimal.ApiClient.Contracts.Requests;
using SaveAnAnimal.ApiClient.Contracts.Responses;
using SaveAnAnimal.ApiClient.Models;
using System.Net;
using System.Net.Http.Json;

namespace SaveAnAnimal.ApiClient;

public class SaveAnAnimalClient
{
	private static readonly IMapper _mapper = new MapperConfiguration(config =>
	{
		config.CreateMap<PetDetailsResponse, Pet>();
		config.CreateMap<VolunteerDetailsResponse, Volunteer>();
	}).CreateMapper();

	private readonly HttpClient _http;
	public string BaseApiUrl { get; }

	public SaveAnAnimalClient(string baseApiUrl, HttpClient? http = null)
	{
		if (baseApiUrl.EndsWith('/'))
		{
			baseApiUrl = baseApiUrl[0..^1];
		}
		BaseApiUrl = baseApiUrl;
		_http = http ?? new HttpClient();
	}

	public async Task<IEnumerable<Pet>> GetPetsAsync()
	{
		string requestUrl = $"{BaseApiUrl}/pet";
		var response = await _http.GetAsync(requestUrl);
		response.EnsureSuccessStatusCode();
		var pets = await response.Content.ReadFromJsonAsync<IEnumerable<PetDetailsResponse>>();
		pets ??= Enumerable.Empty<PetDetailsResponse>();
		return pets.Select(_mapper.Map<Pet>);
	}

	public async Task<Pet?> GetPetAsync(Guid id)
	{
		string requestUrl = $"{BaseApiUrl}/pet/{id}";
		var response = await _http.GetAsync(requestUrl);
		if (response.StatusCode == HttpStatusCode.NotFound)
		{
			return null;
		}
		response.EnsureSuccessStatusCode();
		var petDetails = await response.Content.ReadFromJsonAsync<PetDetailsResponse>();
		return _mapper.Map<Pet>(petDetails);
	}

	public async Task<Pet> CreatePetAsync(PetDetailsRequest request)
	{
		string requestUrl = $"{BaseApiUrl}/pet";
		var response = await _http.PostAsJsonAsync(requestUrl, request);
		response.EnsureSuccessStatusCode();
		var pet = await response.Content.ReadFromJsonAsync<PetDetailsResponse>();
		return _mapper.Map<Pet>(pet);
	}

	public async Task<Pet> UpdatePetAsync(UpdatePetDetailsRequest request)
	{
		string requestUrl = $"{BaseApiUrl}/pet";
		var response = await _http.PutAsJsonAsync(requestUrl, request);
		response.EnsureSuccessStatusCode();
		var pet = await response.Content.ReadFromJsonAsync<PetDetailsResponse>();
		return _mapper.Map<Pet>(pet);
	}

	public async Task DeletePetAsync(Guid id)
	{
		string requestUrl = $"{BaseApiUrl}/pet/{id}";
		var response = await _http.DeleteAsync(requestUrl);
		response.EnsureSuccessStatusCode();
	}

	public async Task<IEnumerable<Volunteer>> GetVolunteersAsync()
	{
		string requestUrl = $"{BaseApiUrl}/volunteer";
		var response = await _http.GetAsync(requestUrl);
		response.EnsureSuccessStatusCode();
		var volunteers = await response.Content.ReadFromJsonAsync<IEnumerable<VolunteerDetailsResponse>>();
		volunteers ??= Enumerable.Empty<VolunteerDetailsResponse>();
		return volunteers.Select(_mapper.Map<Volunteer>);
	}

	public async Task<Volunteer?> GetVolunteerAsync(Guid id)
	{
		string requestUrl = $"{BaseApiUrl}/volunteer/{id}";
		var response = await _http.GetAsync(requestUrl);
		if (response.StatusCode == HttpStatusCode.NotFound)
		{
			return null;
		}
		response.EnsureSuccessStatusCode();
		var volunteerDetails = await response.Content.ReadFromJsonAsync<VolunteerDetailsResponse>();
		return _mapper.Map<Volunteer>(volunteerDetails);
	}

	public async Task<Volunteer> CreateVolunteerAsync(VolunteerDetailsRequest request)
	{
		string requestUrl = $"{BaseApiUrl}/volunteer";
		var response = await _http.PostAsJsonAsync(requestUrl, request);
		response.EnsureSuccessStatusCode();
		var volunteer = await response.Content.ReadFromJsonAsync<VolunteerDetailsResponse>();
		return _mapper.Map<Volunteer>(volunteer);
	}

	public async Task<Volunteer> UpdateVolunteerAsync(Guid id, UpdateVolunteerDetailsRequest request)
	{
		string requestUrl = $"{BaseApiUrl}/volunteer/{id}";
		var response = await _http.PutAsJsonAsync(requestUrl, request);
		response.EnsureSuccessStatusCode();
		var volunteer = await response.Content.ReadFromJsonAsync<VolunteerDetailsResponse>();
		return _mapper.Map<Volunteer>(volunteer);
	}

	public async Task DeleteVolunteerAsync(Guid id)
	{
		string requestUrl = $"{BaseApiUrl}/volunteer/{id}";
		var response = await _http.DeleteAsync(requestUrl);
		response.EnsureSuccessStatusCode();
	}
}
