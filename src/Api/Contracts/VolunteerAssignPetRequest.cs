using System.Text.Json.Serialization;

namespace SaveAnAnimal.Api.Contracts;

public class AssignPetRequest
{
	[JsonConstructor]
	public AssignPetRequest(string petId)
	{
		PetId = petId;
	}

	public string PetId { get; }
}
