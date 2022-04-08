using System.Text.Json.Serialization;

namespace SaveAnAnimal.ApiClient.Contracts;

public class VolunteerDetailsResponse
{
    [JsonConstructor]
    public VolunteerDetailsResponse(string id, string firstName, string? middleName, string lastName, string? email, string? phoneNumber, string? address, string? city)
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

    public string Id { get; }

	public string FirstName { get; }

	public string? MiddleName { get; }

	public string LastName { get; }

	public string? Email { get; }

	public string? PhoneNumber { get; }

	public string? Address { get; }

	public string? City { get; }
}
