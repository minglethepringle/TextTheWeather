using Amazon.Lambda.Core;
using TextTheWeather.Core.Entities.AppUser;
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
	private IWeatherApi WeatherApi = new WeatherGovApi();
	private IWeatherSender EmailSender = new SendGridApi();
	private IWeatherSender SmsSender = new AwsSnsClient();

	public async Task FunctionHandler(AppUser recipient)
	{
		Console.WriteLine($"Processing weather for user: {recipient}");
		// Get weather
		WeatherApiResponse weather =
			await WeatherApi.GetWeather(recipient.Latitude, recipient.Longitude);
		
		IWeatherDataProcessor processor = new WeatherDataProcessor(weather, recipient);

		string weatherDescription = processor.GetWeatherDescription();
		Console.WriteLine(weatherDescription);

		if (recipient.TextWeather)
			await SmsSender.SendWeather(recipient, weatherDescription);

		if (recipient.EmailWeather)
			await EmailSender.SendWeather(recipient, weatherDescription);
	}
}