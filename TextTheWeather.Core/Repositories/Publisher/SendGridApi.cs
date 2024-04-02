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
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("weather@texttheweather.com", "TextTheWeather");
        var subject = $"Weather for {DateTime.Today.ToString("MMMM dd")}";
        var to = new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}");
        var plainTextContent = weatherText;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
        Response sendEmailAsync = await client.SendEmailAsync(msg);
        Console.WriteLine(sendEmailAsync.StatusCode);
    }
}