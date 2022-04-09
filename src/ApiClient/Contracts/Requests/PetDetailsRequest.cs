using SaveAnAnimal.ApiClient.Models;

namespace SaveAnAnimal.ApiClient.Contracts.Requests;

public class PetDetailsRequest
{
	public PetDetailsRequest(string name, PetType type = PetType.Unknown, int? age = null, string? color = null)
	{
		Name = name;
		Type = type;
		Age = age;
		Color = color;
	}

	public string Name { get; set; }

	public PetType Type { get; set; }

	public int? Age { get; set; }

	public string? Color { get; set; }
}
