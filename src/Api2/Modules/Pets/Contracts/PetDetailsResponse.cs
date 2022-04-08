using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SaveAnAnimal.Models;

namespace SaveAnAnimal.Modules.Pets.Contracts;

public class PetDetailsResponse
{
	[Required]
	public string Id { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	[EnumDataType(typeof(PetType))]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PetType Type { get; set; }

	public string? Color { get; set; }

	public PetDetailsResponse(string id, string name, PetType type, string? color)
	{
		Id = id;
		Name = name;
		Type = type;
		Color = color;
	}
}
