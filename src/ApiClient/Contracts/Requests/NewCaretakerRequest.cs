namespace SaveAnAnimal.ApiClient.Contracts.Requests;

public class NewCaretakerRequest
{
	public NewCaretakerRequest(Guid petId, Guid caretakerId)
	{
		PetId = petId;
		CaretakerId = caretakerId;
	}

	public Guid PetId { get; }

	public Guid CaretakerId { get; }
}
