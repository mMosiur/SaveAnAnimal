namespace SaveAnAnimal.ApiClient.Models;

public class PetCare
{
	public Guid Id { get; set; }

	public string PetId { get; set; } = string.Empty;

	public string CaretakerId { get; set; } = string.Empty;

	public DateTime? From { get; set; }

	public DateTime? To { get; set; }
}
