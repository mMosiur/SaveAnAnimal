namespace SaveAnAnimal.ApiClient.Contracts;

public class AssignPetRequest
{
    public AssignPetRequest(string petId)
    {
        PetId = petId;
    }

    public string PetId { get; }
}
