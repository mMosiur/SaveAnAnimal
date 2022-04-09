namespace SaveAnAnimal.ApiClient.Contracts.Responses;

public class VolunteerDetailsResponse
{
	public Guid Id { get; set; }

	public string FirstName { get; set; }

	public string? MiddleName { get; set; }

	public string LastName { get; set; }

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Address { get; set; }

	public string? City { get; set; }

	public VolunteerDetailsResponse(Guid id, string firstName, string? middleName, string lastName, string? email, string? phoneNumber, string? address, string? city)
	{
		Id = id;
		FirstName = firstName;
		MiddleName = middleName;
		LastName = lastName;
		Email = email;
		PhoneNumber = phoneNumber;
		Address = address;
		City = city;
	}
}
