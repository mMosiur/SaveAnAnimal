using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Contracts;

public class VolunteerDetailsRequest
{
	[Required]
	public string FirstName { get; set; } = null!;

	public string? MiddleName { get; set; }

	[Required]
	public string LastName { get; set; } = null!;

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Address { get; set; }

	public string? City { get; set; }
}
