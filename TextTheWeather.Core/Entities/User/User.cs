namespace TextTheWeather.Core.Entities.User;

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public TimeOnly WeatherFrom { get; set; }
    public TimeOnly WeatherTo { get; set; }
    public bool TextWeather { get; set; }
    public bool EmailWeather { get; set; }
    public bool IsPremium { get; set; }
}