using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Contracts.Requests;

public class NewCaretakerRequest
{
	[Required]
	public Guid PetId { get; }

	[Required]
	public Guid CaretakerId { get; }
}
