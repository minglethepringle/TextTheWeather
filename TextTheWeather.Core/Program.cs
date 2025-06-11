using System.Dynamic;
using Amazon;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TextTheWeather.Core.Entities.AppUser;

namespace TextTheWeather.Core;

internal class Program
{
    private static async Task Main(string[] args)
    {
        List<AppUser> recipients = new List<AppUser>();
        recipients.Add(new AppUser
        {
            Id = 2,
            FirstName = "Mingle",
            LastName = "Li",
            Email = "limingle5@gmail.com",
            PhoneNumber = "+17746153717",
            Latitude = "42.384705692336105",
            Longitude = "-71.36785276164706",
            TimezoneOffset = -14400,
            WeatherFrom = new TimeOnly(7, 0),
            WeatherTo = new TimeOnly(22, 0),
            TextWeather = true,
            EmailWeather = false
        });

        // TEST LOCALLY:
        // await new TextTheWeather().Execute(recipients);

        // SIMULATE LOCAL LAMBDA:
        // await new TextTheWeather().FunctionHandler();

        // MAKE NEW USERS:
        // GenerateNewUser();
    }
}