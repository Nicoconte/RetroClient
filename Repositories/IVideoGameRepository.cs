using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	interface IVideoGameRepository
	{
		Task SaveAsync();
		Task<VideoGame> GetSingleAsync(object id);
		Task<IEnumerable<VideoGame>> GetAllAsync();
		Task InsertAsync(VideoGame game);
		Task UpdateAsync(VideoGame game);
		Task DeleteAsync(object id);
	}
}
