using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SaveAnAnimal.Api.Models;

public class Pet
{
	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	[EnumDataType(typeof(PetType))]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PetType Type { get; set; }

	public string? Color { get; set; }
}
