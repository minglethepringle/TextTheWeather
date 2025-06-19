using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;

namespace TextTheWeather.Core.Repositories.Publisher;

public class AwsSnsClient : IWeatherSender
{
    public async Task SendWeather(Entities.AppUser.AppUser user, string weatherText)
    {
        AWSCredentials credentials = new BasicAWSCredentials(
            EnvironmentVariables.AwsAccessKey,
            EnvironmentVariables.AwsSecretKey
        );

        AmazonSimpleNotificationServiceClient snsClient = new(credentials, RegionEndpoint.USEast1);

        PublishRequest request = new()
        {
            Message = weatherText,
            PhoneNumber = user.PhoneNumber
        };

        PublishResponse response = await snsClient.PublishAsync(request);
        Console.WriteLine($"SNS Message ID: {response.MessageId}");
    }
}