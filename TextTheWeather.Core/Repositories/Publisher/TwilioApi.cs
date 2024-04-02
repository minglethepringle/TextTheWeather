using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TextTheWeather.Core.Repositories.Publisher;

public class TwilioApi : IWeatherSender
{
    public async Task SendWeather(User user, string weatherText)
    {
        var accountSid = EnvironmentVariables.TwilioAccountSid;
        var authToken = EnvironmentVariables.TwilioAuthToken;
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
            new PhoneNumber($"+1{user.PhoneNumber}"));
        messageOptions.From = new PhoneNumber(EnvironmentVariables.TwilioPhoneNumber);
        messageOptions.Body = weatherText;

        var message = await MessageResource.CreateAsync(messageOptions);
        Console.WriteLine(message.Body);
    }
}