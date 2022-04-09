namespace SaveAnAnimal.ApiClient.Models;

public class Pet
{
	public Pet(Guid id, string name, string type, string? color)
	{
		Id = id;
		Name = name;
		Type = type;
		Color = color;
	}

	public Pet(Guid id, string name, string type)
		: this(id, name, type, null)
	{
	}

	public Guid Id { get; }

	public string Name { get; }

	public string Type { get; }

	public string? Color { get; }
}
