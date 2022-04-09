namespace SaveAnAnimal.ApiClient.Models;

public class PetCare
{
	public Guid Id { get; set; }

	public Guid PetId { get; set; }

	public Guid CaretakerId { get; set; }

	public DateTime? From { get; set; }

	public DateTime? To { get; set; }
}
