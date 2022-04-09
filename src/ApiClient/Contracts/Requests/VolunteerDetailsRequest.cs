using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.ApiClient.Contracts.Requests;

public class VolunteerDetailsRequest
{
    public VolunteerDetailsRequest(string firstName, string? middleName, string lastName, string? email, string? phoneNumber, string? address, string? city)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        City = city;
    }

    public string FirstName { get; set; } = null!;

	public string? MiddleName { get; set; }

	public string LastName { get; set; } = null!;

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Address { get; set; }

	public string? City { get; set; }
}
