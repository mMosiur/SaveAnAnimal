using SaveAnAnimal.ApiClient.Contracts;

namespace SaveAnAnimal.ApiClient.Models;

public class Pet
{
    public Pet(string id, string name, string type, string? color)
    {
        Id = id;
        Name = name;
        Type = type;
        Color = color;
    }

    public Pet(string id, string name, string type)
        : this(id, name, type, null)
    {
    }

    public string Id { get; }

	public string Name { get; }

	public string Type { get; }

	public string? Color { get; }

    public static Pet FromPetDetailsResponse(PetDetailsResponse details) => new(details.Id, details.Name, details.Type, details.Color);
}
