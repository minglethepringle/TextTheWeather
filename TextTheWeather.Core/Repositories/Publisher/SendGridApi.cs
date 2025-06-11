using SendGrid;
using SendGrid.Helpers.Mail;
using TextTheWeather.Core.Helpers;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;

namespace TextTheWeather.Core.Repositories.Publisher;

public class SendGridApi : IWeatherSender
{
	public async Task SendWeather(Entities.AppUser.AppUser appUser, string weatherText)
	{
		string apiKey = EnvironmentVariables.SendGridApiKey;
		SendGridClient client = new(apiKey);

		EmailAddress from = new("weather@texttheweather.com", "TextTheWeather");
		string today = LocalDateTime.LocalNow(appUser.TimezoneOffset).ToString("MMMM dd");
		string subject = $"Weather for {today}";
		EmailAddress to = new(appUser.Email, $"{appUser.FirstName} {appUser.LastName}");

		SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, weatherText, null);
		Response sendEmailAsync = await client.SendEmailAsync(msg);
		Console.WriteLine(sendEmailAsync.StatusCode);
	}
}