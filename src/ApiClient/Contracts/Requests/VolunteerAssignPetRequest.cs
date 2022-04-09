namespace SaveAnAnimal.ApiClient.Contracts.Requests;

public class VolunteerAssignPetRequest
{
    public VolunteerAssignPetRequest(Guid petId)
    {
        PetId = petId;
    }

    public Guid PetId { get; set; }
}
