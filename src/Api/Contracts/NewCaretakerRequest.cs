using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SaveAnAnimal.Api.Contracts;

public class NewCaretakerRequest
{
	[JsonConstructor]
	public NewCaretakerRequest(string petId, string caretakerId)
	{
		PetId = petId;
		CaretakerId = caretakerId;
	}

	[Required]
	public string PetId { get; } = string.Empty;

	[Required]
	public string CaretakerId { get; } = string.Empty;
}
