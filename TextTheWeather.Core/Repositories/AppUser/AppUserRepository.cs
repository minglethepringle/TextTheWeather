using Supabase;
using Supabase.Postgrest.Responses;
using TextTheWeather.Core.Repositories.Interfaces.AppUser;

namespace TextTheWeather.Core.Repositories.AppUser;

public class AppUserRepository : IAppUserRepository
{
	public async Task<List<Entities.AppUser.AppUser>> GetUsersAsync()
	{
		Client supabaseDb = new Client(EnvironmentVariables.SupabaseUrl, EnvironmentVariables.SupabaseApiKey);
		await supabaseDb.InitializeAsync();

		ModeledResponse<Entities.AppUser.AppUser> result = await supabaseDb.From<Entities.AppUser.AppUser>().Get();
		return result.Models
			.OrderBy(user => user.Id)
			.ToList();
	}
}