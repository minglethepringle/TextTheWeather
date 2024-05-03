using TextTheWeather.Core.Entities.AppUser;

namespace TextTheWeather.Api.Controllers.Interfaces;

public interface IUserController
{
	void CreateUser(AppUser user);
	void UpdateUser(AppUser user);
	void DeleteUser(AppUser user);
	AppUser GetUserById(int id);
}