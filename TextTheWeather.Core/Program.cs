using TextTheWeather.Core.Entities.User;

namespace TextTheWeather.Core;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var recipients = new List<User>();
		recipients.Add(new User
		{
			UserId = Guid.Parse("c61fc2cc-bcd3-440f-b975-ddc47e536ea0"),
			FirstName = "Mingle",
			LastName = "Li",
			Email = "limingle5@gmail.com",
			PhoneNumber = "+17746153717",
			Latitude = "38.963421335750915",
			Longitude = "-77.08732715397758",
			TimezoneOffset = -14400,
			WeatherFrom = new TimeOnly(7, 0),
			WeatherTo = new TimeOnly(20, 0),
			TextWeather = true,
			EmailWeather = false,
			IsPremium = true
		});

		// TEST LOCALLY:
		await new TextTheWeather().Execute(recipients);

		// MAKE NEW USERS:
		// GenerateNewUser();
	}

	private static void GenerateNewUser()
	{
		Console.WriteLine(new User
		{
			UserId = Guid.NewGuid(),
			FirstName = "Walker",
			LastName = "Whitehouse",
			Email = "walker.whitehouse@gmail.com",
			PhoneNumber = "+17817952773",
			Latitude = "41.29370682483185",
			Longitude = "-82.21649535909195",
			TimezoneOffset = -14400,
			WeatherFrom = new TimeOnly(11, 0),
			WeatherTo = new TimeOnly(20, 0),
			TextWeather = true,
			EmailWeather = false,
			IsPremium = false
		});
	}
}