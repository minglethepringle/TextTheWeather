using System.Text;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace TextTheWeather.Core.Entities.AppUser;

[Table("user")]
public class AppUser : BaseModel
{
	[PrimaryKey("id")] public int Id { get; set; }
	[Column("first_name")] public string FirstName { get; set; }
	[Column("last_name")] public string LastName { get; set; }
	[Column("email")] public string Email { get; set; }
	[Column("phone_number")] public string PhoneNumber { get; set; }
	[Column("latitude")] public string Latitude { get; set; }
	[Column("longitude")] public string Longitude { get; set; }
	[Column("timezone_offset")] public double TimezoneOffset { get; set; }
	[Column("weather_from")] public TimeOnly WeatherFrom { get; set; }
	[Column("weather_to")] public TimeOnly WeatherTo { get; set; }
	[Column("text_weather")] public bool TextWeather { get; set; }
	[Column("email_weather")] public bool EmailWeather { get; set; }

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