using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api.Contracts.Requests;

public class UpdatePetDetailsRequest
{
	public string? Name { get; set; }

	[EnumDataType(typeof(PetType))]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PetType? Type { get; set; }

	public string? Color { get; set; }
}
