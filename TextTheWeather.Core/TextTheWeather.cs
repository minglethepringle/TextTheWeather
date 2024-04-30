using Amazon.Lambda.Core;
using TextTheWeather.Core.Entities.AppUser;
using TextTheWeather.Core.Entities.WeatherApi;
using TextTheWeather.Core.Processors;
using TextTheWeather.Core.Processors.Interfaces;
using TextTheWeather.Core.Repositories.AppUser;
using TextTheWeather.Core.Repositories.Interfaces.AppUser;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;
using TextTheWeather.Core.Repositories.Interfaces.Weather;
using TextTheWeather.Core.Repositories.Publisher;
using TextTheWeather.Core.Repositories.Weather;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TextTheWeather.Core;

public class TextTheWeather
{
	private IAppUserRepository AppUserRepository = new AppUserRepository();
	private IWeatherSender EmailSender = new SendGridApi();
	private IWeatherSender SmsSender = new TwilioApi();
	private IWeatherApiFactory WeatherApiFactory = new WeatherApiFactory();

	/**
	 * Handles the lambda function and input from Supabase
	 */
	public async Task FunctionHandler()
	{
		List<AppUser> recipients = await AppUserRepository.GetUsersAsync();

		await Execute(recipients);
	}

	public async Task Execute(List<AppUser> recipients)
	{
		foreach (AppUser recipient in recipients)
		{
			// Get weather
			WeatherApiResponse weather =
				await WeatherApiFactory.Create().GetWeather(recipient.Latitude, recipient.Longitude);

			IWeatherDataProcessor processor = new WeatherDataProcessor(weather, recipient);

			string weatherDescription = processor.GetWeatherDescription();
			Console.WriteLine(weatherDescription);

			if (recipient.TextWeather)
				await SmsSender.SendWeather(recipient, weatherDescription);

			if (recipient.EmailWeather)
				await EmailSender.SendWeather(recipient, weatherDescription);
		}
	}
}