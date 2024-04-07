using SendGrid;
using SendGrid.Helpers.Mail;
using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Helpers;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;

namespace TextTheWeather.Core.Repositories.Publisher;

public class SendGridApi : IWeatherSender
{
	public async Task SendWeather(User user, string weatherText)
	{
		var apiKey = EnvironmentVariables.SendGridApiKey;
		SendGridClient client = new SendGridClient(apiKey);

		EmailAddress from = new EmailAddress("weather@texttheweather.com", "TextTheWeather");
		var today = LocalDateTime.LocalNow(user.TimezoneOffset).ToString("MMMM dd");
		var subject = $"Weather for {today}";
		EmailAddress to = new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}");

		SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, weatherText, null);
		Response sendEmailAsync = await client.SendEmailAsync(msg);
		Console.WriteLine(sendEmailAsync.StatusCode);
	}
}