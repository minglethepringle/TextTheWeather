using TextTheWeather.Core.Entities.AppUser;

namespace TextTheWeather.Core;

internal class Program
{
	private static async Task Main(string[] args)
	{
		List<AppUser> recipients = new List<AppUser>();
		recipients.Add(new AppUser
		{
			Id = 2,
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
			EmailWeather = false
		});

		// TEST LOCALLY:
		await new TextTheWeather().Execute(recipients);

		// SIMULATE LOCAL LAMBDA:
		// await new TextTheWeather().FunctionHandler();

		// MAKE NEW USERS:
		// GenerateNewUser();
	}

	private static void GenerateNewUser()
	{
		Console.WriteLine(new AppUser
		{
			Id = 235,
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
			EmailWeather = false
		});
	}
}