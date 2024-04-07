namespace TextTheWeather.Core.Helpers;

public class LocalDateTime
{
	public static DateTime FromUnixTime(long unixTime, double timezoneOffset)
	{
		return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime.AddSeconds(timezoneOffset);
	}

	public static DateTime LocalNow(double timezoneOffset)
	{
		return DateTime.UtcNow.AddSeconds(timezoneOffset);
	}
}