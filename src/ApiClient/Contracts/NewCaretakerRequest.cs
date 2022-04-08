namespace SaveAnAnimal.ApiClient.Contracts;

public class NewCaretakerRequest
{
    public NewCaretakerRequest(string petId, string caretakerId)
    {
        PetId = petId;
        CaretakerId = caretakerId;
    }

    public string PetId { get; }

	public string CaretakerId { get; }
}
