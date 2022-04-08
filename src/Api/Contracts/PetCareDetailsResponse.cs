using SaveAnAnimal.Api.Models;

namespace SaveAnAnimal.Api.Contracts;

public class PetCareDetailsResponse
{
	public string PetId { get; }

	public string CaretakerId { get; }

	public DateTime? From { get; }

	public DateTime? To { get; }

	public PetCareDetailsResponse(string petId, string caretakerId, DateTime? from, DateTime? to)
	{
		PetId = petId;
		CaretakerId = caretakerId;
		From = from;
		To = to;
	}

	public PetCareDetailsResponse(Guid petId, Guid caretakerId, DateTime? from, DateTime? to)
		: this(petId.ToUrlString(), caretakerId.ToUrlString(), from, to)
	{
	}

	public static PetCareDetailsResponse BuildFrom(PetCare petCare) => new(petCare.Pet.Id, petCare.Caretaker.Id, petCare.From, petCare.To);
}
