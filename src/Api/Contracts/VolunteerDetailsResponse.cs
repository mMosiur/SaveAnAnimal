using SaveAnAnimal.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace SaveAnAnimal.Api.Contracts;

public class VolunteerDetailsResponse
{
	public string Id { get; set; } = string.Empty;

	public string FirstName { get; set; } = null!;

	public string? MiddleName { get; set; }

	public string LastName { get; set; } = null!;

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Address { get; set; }

	public string? City { get; set; }

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

	public VolunteerDetailsResponse(Guid id, string firstName, string? middleName, string lastName, string? email, string? phoneNumber, string? address, string? city)
		: this(id.ToUrlString(), firstName, middleName, lastName, email, phoneNumber, address, city)
	{
	}

	public static VolunteerDetailsResponse BuildFrom(Volunteer volunteer) => new(volunteer.Id, volunteer.FirstName, volunteer.MiddleName, volunteer.LastName, volunteer.Email, volunteer.PhoneNumber, volunteer.Address, volunteer.City);
}
