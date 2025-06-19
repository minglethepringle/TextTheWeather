using System.Text;
using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.AppUser;

public class AppUser
{
	[JsonPropertyName("id")] public int Id { get; set; }
	[JsonPropertyName("first_name")] public string FirstName { get; set; }
	[JsonPropertyName("last_name")] public string LastName { get; set; }
	[JsonPropertyName("email")] public string Email { get; set; }
	[JsonPropertyName("phone_number")] public string PhoneNumber { get; set; }
	[JsonPropertyName("latitude")] public string Latitude { get; set; }
	[JsonPropertyName("longitude")] public string Longitude { get; set; }
	[JsonPropertyName("timezone_offset")] public double TimezoneOffset { get; set; }
	[JsonPropertyName("weather_from")] public TimeOnly WeatherFrom { get; set; }
	[JsonPropertyName("weather_to")] public TimeOnly WeatherTo { get; set; }
	[JsonPropertyName("text_weather")] public bool TextWeather { get; set; }
	[JsonPropertyName("email_weather")] public bool EmailWeather { get; set; }

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();

		sb.AppendLine("User: {");
		sb.AppendLine($"  UserId: {Id}");
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
		sb.AppendLine("}");

		return sb.ToString();
	}
}