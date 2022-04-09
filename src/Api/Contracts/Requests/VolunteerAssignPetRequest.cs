using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Contracts.Requests;

public class VolunteerAssignPetRequest
{
	[Required]
	public Guid PetId { get; set; }
}
