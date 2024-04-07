using Amazon.Lambda.Core;
using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Entities.WeatherApi;
using TextTheWeather.Core.Processors;
using TextTheWeather.Core.Processors.Interfaces;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;
using TextTheWeather.Core.Repositories.Interfaces.Weather;
using TextTheWeather.Core.Repositories.Publisher;
using TextTheWeather.Core.Repositories.Weather;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TextTheWeather.Core;

public class TextTheWeather
{
	private IWeatherSender EmailSender = new SendGridApi();
	private IWeatherSender SmsSender = new TwilioApi();
	private IWeatherApiFactory WeatherApiFactory = new WeatherApiFactory();

	public async Task FunctionHandler()
	{
		var recipients = new List<User>();
		recipients.Add(new User
		{
			FirstName = "Mingle",
			LastName = "Li",
			Email = "limingle5@gmail.com",
			PhoneNumber = "7746153717",
			Latitude = "38.963421335750915",
			Longitude = "-77.08732715397758",
			WeatherFrom = new TimeOnly(7, 0),
			WeatherTo = new TimeOnly(20, 0),
			TextWeather = true,
			EmailWeather = true,
			IsPremium = true
		});
		recipients.Add(new User
		{
			FirstName = "Lilli",
			LastName = "Tobe",
			Email = "lilli.tobe@gmail.com",
			PhoneNumber = "7742906766",
			Latitude = "38.90072056045205",
			Longitude = "-77.0497969457071",
			WeatherFrom = new TimeOnly(7, 0),
			WeatherTo = new TimeOnly(20, 0),
			TextWeather = true,
			EmailWeather = true,
			IsPremium = true
		});
		recipients.Add(new User
		{
			FirstName = "Zach",
			LastName = "Simon",
			Email = "zach.simon027@gmail.com",
			PhoneNumber = "7202336158",
			Latitude = "38.904169160076826",
			Longitude = "-77.02937033057403",
			WeatherFrom = new TimeOnly(7, 0),
			WeatherTo = new TimeOnly(20, 0),
			TextWeather = true,
			EmailWeather = true,
			IsPremium = true
		});

		foreach (User recipient in recipients)
		{
			// Get weather
			WeatherApiResponse weather =
				await WeatherApiFactory.Create().GetWeather(recipient.Latitude, recipient.Longitude);

			IWeatherDataProcessor processor = new WeatherDataProcessor(weather, recipient);

			var weatherDescription = processor.GetWeatherDescription();
			Console.WriteLine(weatherDescription);

			if (recipient.TextWeather)
				await SmsSender.SendWeather(recipient, weatherDescription);

			if (recipient.EmailWeather)
				await EmailSender.SendWeather(recipient, weatherDescription);
		}
	}
}