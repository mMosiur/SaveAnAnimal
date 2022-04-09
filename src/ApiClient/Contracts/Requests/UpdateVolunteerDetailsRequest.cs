namespace SaveAnAnimal.ApiClient.Contracts.Requests;

public class UpdateVolunteerDetailsRequest
{
	public string? FirstName { get; set; }

	public string? MiddleName { get; set; }

	public string? LastName { get; set; }

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Address { get; set; }

	public string? City { get; set; }
}
