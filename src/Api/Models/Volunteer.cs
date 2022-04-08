using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Models;

public class Volunteer
{
	[Required]
	public Guid Id { get; set; }

	[Required]
	public string FirstName { get; set; } = null!;

	public string? MiddleName { get; set; }

	[Required]
	public string LastName { get; set; } = null!;

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Address { get; set; }

	public string? City { get; set; }

	public string FullName => MiddleName is null ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";
}
