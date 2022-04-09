using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Models;

public class PetCare
{
	public Guid Id { get; set; }

	public Guid PetId { get; set; }
	[Required]
	public Pet Pet { get; set; } = null!;

	public Guid CaretakerId { get; set; }
	[Required]
	public Volunteer Caretaker { get; set; } = null!;

	public DateTime? From { get; set; }

	public DateTime? To { get; set; }
}
