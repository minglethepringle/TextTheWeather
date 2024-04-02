namespace TextTheWeather.Core.Mappers.Interfaces;

public interface IWeatherDataMapper<TFrom, TTo>
{
    TTo Map(TFrom from, double timezoneOffset);
}