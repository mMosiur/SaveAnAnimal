using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Contracts.Requests;

public class AssignPetRequest
{
	[Required]
	public Guid PetId { get; set; }
}
