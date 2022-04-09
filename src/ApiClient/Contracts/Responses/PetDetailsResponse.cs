namespace SaveAnAnimal.ApiClient.Contracts.Responses;

public class PetDetailsResponse
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public string Type { get; set; }

	public int? Age { get; set; }

	public string? Color { get; set; }

	public PetDetailsResponse(Guid id, string name, string type, int? age, string? color)
	{
		Id = id;
		Name = name;
		Type = type;
		Age = age;
		Color = color;
	}
}
