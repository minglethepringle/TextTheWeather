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
			EmailWeather = true,
			IsPremium = true
		});
		// recipients.Add(new User
		// {
		// 	FirstName = "Lilli",
		// 	LastName = "Tobe",
		// 	Email = "lilli.tobe@gmail.com",
		// 	PhoneNumber = "+17742906766",
		// 	Latitude = "38.90072056045205",
		// 	Longitude = "-77.0497969457071",
		// 	WeatherFrom = new TimeOnly(7, 0),
		// 	WeatherTo = new TimeOnly(20, 0),
		// 	TextWeather = true,
		// 	EmailWeather = true,
		// 	IsPremium = true
		// });
		// recipients.Add(new User
		// {
		// 	FirstName = "Zach",
		// 	LastName = "Simon",
		// 	Email = "zach.simon027@gmail.com",
		// 	PhoneNumber = "+17202336158",
		// 	Latitude = "38.904169160076826",
		// 	Longitude = "-77.02937033057403",
		// 	WeatherFrom = new TimeOnly(7, 0),
		// 	WeatherTo = new TimeOnly(20, 0),
		// 	TextWeather = true,
		// 	EmailWeather = true,
		// 	IsPremium = true
		// });
		await new TextTheWeather().FunctionHandler(recipients);
	}
}