using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SaveAnAnimal.ApiClient.Models;

namespace SaveAnAnimal.ApiClient.Contracts.Requests;

public class UpdatePetDetailsRequest
{
	public string? Name { get; set; }

	[EnumDataType(typeof(PetType))]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PetType? Type { get; set; }

	public int? Age { get; set; }

	public string? Color { get; set; }
}
