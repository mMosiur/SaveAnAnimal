namespace SaveAnAnimal.Api.Contracts.Responses;

public class PetCareDetailsResponse
{
	public Guid PetId { get; set; }

	public Guid CaretakerId { get; set; }

	public DateTime? From { get; set; }

	public DateTime? To { get; set; }

	public PetCareDetailsResponse(Guid petId, Guid caretakerId, DateTime? from, DateTime? to)
	{
		PetId = petId;
		CaretakerId = caretakerId;
		From = from;
		To = to;
	}
}
