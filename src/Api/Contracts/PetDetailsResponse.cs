using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api.Contracts;

public class PetDetailsResponse
{
	public string Id { get; }

	public string Name { get; }

	public string Type { get; }

	public string? Color { get; }

	public PetDetailsResponse(string id, string name, string type, string? color)
	{
		Id = id;
		Name = name;
		Type = type;
		Color = color;
	}

	public PetDetailsResponse(Guid id, string name, string type, string? color)
		: this(id.ToUrlString(), name, type, color)
	{
	}

	public static PetDetailsResponse BuildFrom(Pet pet) => new(pet.Id, pet.Name, pet.Type.ToString(), pet.Color);
}
