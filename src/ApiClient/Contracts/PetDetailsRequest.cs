namespace SaveAnAnimal.ApiClient.Contracts;

public class PetDetailsRequest
{
    public PetDetailsRequest(string name, string type, string? color)
    {
        Name = name;
        Type = type;
        Color = color;
    }

    public PetDetailsRequest(string name, string type)
        : this(name, type, null)
    {
    }

    public string Name { get; set; }

	public string Type { get; set; }

	public string? Color { get; set; }
}
