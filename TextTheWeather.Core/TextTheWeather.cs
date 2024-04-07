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

	public async Task FunctionHandler(List<User> recipients)
	{
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