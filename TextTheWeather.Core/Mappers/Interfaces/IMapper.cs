namespace TextTheWeather.Core.Mappers.Interfaces;

public interface IMapper<TFrom, TTo>
{
    TTo Map(TFrom from);
}