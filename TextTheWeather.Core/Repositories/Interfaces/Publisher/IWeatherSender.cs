namespace TextTheWeather.Core.Repositories.Interfaces.Publisher;

public interface IWeatherSender
{
	Task SendWeather(Entities.AppUser.AppUser user, string weatherText);
}