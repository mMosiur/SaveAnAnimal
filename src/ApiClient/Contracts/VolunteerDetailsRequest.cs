namespace SaveAnAnimal.ApiClient.Contracts;

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

    public VolunteerDetailsRequest(string firstName, string lastName)
        : this(firstName, null, lastName, null, null, null, null)
    {
    }

    public string FirstName { get; }

	public string? MiddleName { get; }

	public string LastName { get; }

	public string? Email { get; }

	public string? PhoneNumber { get; }

	public string? Address { get; }

	public string? City { get; }
}
