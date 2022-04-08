using System.Text.Json.Serialization;

namespace SaveAnAnimal.ApiClient.Contracts;

public class PetCareDetailsResponse
{
    [JsonConstructor]
    public PetCareDetailsResponse(string petId, string caretakerId, DateTime? from, DateTime? to)
    {
        PetId = petId;
        CaretakerId = caretakerId;
        From = from;
        To = to;
    }

    public string PetId { get; }

	public string CaretakerId { get; }

	public DateTime? From { get; }

	public DateTime? To { get; }
}
