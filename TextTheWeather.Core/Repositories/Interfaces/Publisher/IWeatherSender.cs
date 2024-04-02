using TextTheWeather.Core.Entities.User;

namespace TextTheWeather.Core.Repositories.Interfaces.Publisher;

public interface IWeatherSender
{
    Task SendWeather(User user, string weatherText);
}