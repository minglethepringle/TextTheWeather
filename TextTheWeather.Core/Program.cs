using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Mappers;
using TextTheWeather.Core.Mappers.Interfaces;
using TextTheWeather.Core.Repositories.Interfaces.Publisher;
using TextTheWeather.Core.Repositories.Interfaces.WeatherRepository;
using TextTheWeather.Core.Repositories.Publisher;
using TextTheWeather.Core.Repositories.Weather;

namespace TextTheWeather.Core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            bool test = true;
            User recipient = new()
            {
                FirstName = test ? "Mingle" : "Lilli",
                LastName = test ? "Li" : "Tobe",
                Email = test ? "limingle5@gmail.com" : "lilli.tobe@gmail.com",
                PhoneNumber = test ? "7746153717" : "7742906766",
                Latitude = "38.90072056045205",
                Longitude = "-77.0497969457071",
                WeatherFrom = new TimeOnly(7, 0),
                WeatherTo = new TimeOnly(20, 0),
                TextWeather = true,
                EmailWeather = true,
                IsPremium = true
            };
            
            // Get weather from OpenWeather API
            IWeatherApi weatherApi = new OpenWeatherApi();
            OpenWeatherApiResponse weather = weatherApi.GetWeather(recipient.Latitude, recipient.Longitude).Result;
            
            // Map
            IMapper<OpenWeatherApiResponse, string> mapper = new WeatherToHumanMapper(recipient);
            string humanReadableWeather = mapper.Map(weather);
            Console.WriteLine(humanReadableWeather);
            
            // Send weather to Twilio API
            IWeatherSender sender = new SendGridApi();
            
            // ITwilioApi twilioApi = new TwilioApi();
            // twilioApi.SendSms("8777804236", humanReadableWeather);
            await sender.SendWeather(recipient, humanReadableWeather);
        }
    }
}