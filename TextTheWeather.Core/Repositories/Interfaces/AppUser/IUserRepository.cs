namespace TextTheWeather.Core.Repositories.Interfaces.AppUser;

public interface IAppUserRepository
{
	Task<List<Entities.AppUser.AppUser>> GetUsersAsync();
}