using SaveAnAnimal.ApiClient.Contracts;
using SaveAnAnimal.ApiClient.Models;
using System.Net;
using System.Net.Http.Json;

namespace SaveAnAnimal.ApiClient;

public class SaveAnAnimalClient
{
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

    public async Task<List<Pet>> GetPetsAsync()
    {
        var requestUrl = $"{BaseApiUrl}/pet";
        var response = await _http.GetFromJsonAsync<IEnumerable<PetDetailsResponse>>(requestUrl);
        if(response is null)
        {
            return new List<Pet>();
        }
        return response.Select(Pet.FromPetDetailsResponse).ToList();
    }

    public async Task<Pet?> GetPetAsync(string id)
    {
        var requestUrl = $"{BaseApiUrl}/pet/{id}";
        var response = await _http.GetAsync(requestUrl);
        if(response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }
        var petDetails = await response.Content.ReadFromJsonAsync<PetDetailsResponse>();
        if(petDetails is null)
        {
            return null;
        }
        return Pet.FromPetDetailsResponse(petDetails);
    }

    public async Task<Pet?> CreatePetAsync(PetDetailsRequest details)
    {
        var requestUrl = $"{BaseApiUrl}/pet";
        var response = await _http.PostAsJsonAsync(requestUrl, details);
        var pet = await response.Content.ReadFromJsonAsync<PetDetailsResponse>();
        if (pet is null)
        {
            return null;
        }
        return Pet.FromPetDetailsResponse(pet);
    }

    public async Task<Pet?> UpdatePetAsync(PetDetailsRequest details)
    {
        var requestUrl = $"{BaseApiUrl}/pet";
        var response = await _http.PutAsJsonAsync(requestUrl, details);
        var pet = await response.Content.ReadFromJsonAsync<PetDetailsResponse>();
        if (pet is null)
        {
            return null;
        }
        return Pet.FromPetDetailsResponse(pet);
    }
}
