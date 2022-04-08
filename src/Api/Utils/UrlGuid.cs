namespace SaveAnAnimal.Api;

public static class UrlGuid
{
	public static string ToUrlString(this Guid id)
	{
		byte[] bytes = id.ToByteArray();
		string encodedStr = Convert.ToBase64String(bytes)
			.Replace('/', '_')
			.Replace('+', '-')
			.TrimEnd('=');
		return encodedStr;
	}

	public static Guid FromUrlString(string s)
	{
		string encodedStr = s
			.Replace('_', '/')
			.Replace('-', '+')
			+ "==";
		byte[] bytes = Convert.FromBase64String(encodedStr);
		return new Guid(bytes);
	}

	public static bool TryFromUrlString(string s, out Guid id)
	{
		try
		{
			id = FromUrlString(s);
			return true;
		}
		catch (ArgumentException)
		{
			id = default;
			return false;
		}
		catch (FormatException)
		{
			id = default;
			return false;
		}
	}
}
