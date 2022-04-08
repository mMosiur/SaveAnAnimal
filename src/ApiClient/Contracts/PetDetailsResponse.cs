using System.Text.Json.Serialization;

namespace SaveAnAnimal.ApiClient.Contracts;

public class PetDetailsResponse
{
    [JsonConstructor]
    public PetDetailsResponse(string id, string name, string type, string color)
    {
        Id = id;
        Name = name;
        Type = type;
        Color = color;
    }

    public string Id { get; }

	public string Name { get; }

	public string Type { get; }

	public string Color { get; }
}
