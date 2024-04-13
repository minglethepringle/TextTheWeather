using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
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

	/**
	 * Handles the lambda function and input from DynamoDB
	 */
	public async Task FunctionHandler()
	{
		AmazonDynamoDBClient client = new AmazonDynamoDBClient(EnvironmentVariables.AwsAccessKeyId, EnvironmentVariables.AwsSecretAccessKey, RegionEndpoint.USEast1);

		ScanRequest request = new ScanRequest
		{
			TableName = "TextTheWeather_Users"
		};

		ScanResponse response = await client.ScanAsync(request);
		var result = response.Items;

		var recipients = new List<User>();
		foreach (var item in result)
		{
			Document document = Document.FromAttributeMap(item);
			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			User user = JsonSerializer.Deserialize<User>(document.ToJsonPretty(), options);
			recipients.Add(user);
		}

		await Execute(recipients);

		// Console.WriteLine("Received database result");
		// Console.WriteLine($"Scan count: {databaseResult.Count}");
		// // var recipients = databaseResult.Items
		// // 	.Select(Document.FromAttributeMap) // Convert each dictionary to a Document
		// // 	.Select(document => JsonSerializer.Deserialize<User>(document.ToJsonPretty())) // Deserialize each document to a User
		// // 	.ToList(); // Convert the IEnumerable to a List
		//
		// var recipients = new List<User>();
		//
		// foreach (var item in databaseResult.Items)
		// {
		// 	Document document = Document.FromAttributeMap(item);
		// 	User user = JsonSerializer.Deserialize<User>(document.ToJsonPretty());
		// 	recipients.Add(user);
		// }
		//
		// Console.WriteLine($"Recipients: {JsonSerializer.Serialize(recipients)}");
		//
		// await Execute(recipients);
	}

	public async Task Execute(List<User> recipients)
	{
		foreach (User recipient in recipients)
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