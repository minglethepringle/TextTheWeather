using SendGrid;
using SendGrid.Helpers.Mail;
using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;

namespace TextTheWeather.Core.Repositories.Publisher;

public class SendGridApi : IWeatherSender
{
	public async Task SendWeather(User user, string weatherText)
	{
		var apiKey = EnvironmentVariables.SendGridApiKey;
		SendGridClient client = new SendGridClient(apiKey);
		EmailAddress from = new EmailAddress("weather@texttheweather.com", "TextTheWeather");
		var subject = $"Weather for {DateTime.Today.ToString("MMMM dd")}"; // Known bug: this is in UTC so it may be off by a day
		EmailAddress to = new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}");
		var plainTextContent = weatherText;
		SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
		Response sendEmailAsync = await client.SendEmailAsync(msg);
		Console.WriteLine(sendEmailAsync.StatusCode);
	}
}