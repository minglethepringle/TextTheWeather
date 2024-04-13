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
		// Console.WriteLine(new User
		// {
		// 	UserId = Guid.NewGuid(),
		// 	FirstName = "Jiming",
		// 	LastName = "Xu",
		// 	Email = "jimingwxu@gmail.com",
		// 	PhoneNumber = "+15082399085",
		// 	Latitude = "42.37151655138981",
		// 	Longitude = "-72.51485503205531",
		// 	TimezoneOffset = -14400,
		// 	WeatherFrom = new TimeOnly(9, 0),
		// 	WeatherTo = new TimeOnly(23, 0),
		// 	TextWeather = true,
		// 	EmailWeather = false,
		// 	IsPremium = false
		// });
	}
}