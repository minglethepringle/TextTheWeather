using System.Text;

namespace TextTheWeather.Core.Entities.User;

public class User
{
	public Guid UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public string Latitude { get; set; }
	public string Longitude { get; set; }
	public double TimezoneOffset { get; set; }
	public TimeOnly WeatherFrom { get; set; }
	public TimeOnly WeatherTo { get; set; }
	public bool TextWeather { get; set; }
	public bool EmailWeather { get; set; }
	public bool IsPremium { get; set; }

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();

		sb.AppendLine("User: {");
		sb.AppendLine($"  UserId: {UserId}");
		sb.AppendLine($"  FirstName: {FirstName}");
		sb.AppendLine($"  LastName: {LastName}");
		sb.AppendLine($"  Email: {Email}");
		sb.AppendLine($"  PhoneNumber: {PhoneNumber}");
		sb.AppendLine($"  Latitude: {Latitude}");
		sb.AppendLine($"  Longitude: {Longitude}");
		sb.AppendLine($"  TimezoneOffset: {TimezoneOffset}");
		sb.AppendLine($"  WeatherFrom: {WeatherFrom.ToString("HH:mm:ss")}");
		sb.AppendLine($"  WeatherTo: {WeatherTo.ToString("HH:mm:ss")}");
		sb.AppendLine($"  TextWeather: {TextWeather}");
		sb.AppendLine($"  EmailWeather: {EmailWeather}");
		sb.AppendLine($"  IsPremium: {IsPremium}");
		sb.AppendLine("}");

		return sb.ToString();
	}
}