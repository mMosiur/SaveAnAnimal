using System.Runtime.Serialization;

namespace SaveAnAnimal.Models;

public enum PetType
{
	[EnumMember(Value = "unknown")]
	Unknown,

	[EnumMember(Value = "dog")]
	Dog,

	[EnumMember(Value = "cat")]
	Cat
}
