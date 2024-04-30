using TextTheWeather.Core.Repositories.Interfaces.Publisher;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TextTheWeather.Core.Repositories.Publisher;

public class TwilioApi : IWeatherSender
{
	public async Task SendWeather(Entities.AppUser.AppUser appUser, string weatherText)
	{
		string accountSid = EnvironmentVariables.TwilioAccountSid;
		string authToken = EnvironmentVariables.TwilioAuthToken;
		TwilioClient.Init(accountSid, authToken);

		CreateMessageOptions messageOptions = new CreateMessageOptions(
			new PhoneNumber(appUser.PhoneNumber));
		messageOptions.From = new PhoneNumber(EnvironmentVariables.TwilioPhoneNumber);
		messageOptions.Body = weatherText;

		MessageResource resource = await MessageResource.CreateAsync(messageOptions);
		Console.WriteLine(resource.Status);
	}
}