namespace TextTheWeather.Core.Helpers;

public static class StringExtension
{
	public static string ToTitleCase(this string str)
	{
		return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
	}
}