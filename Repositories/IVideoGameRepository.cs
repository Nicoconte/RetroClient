using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	interface IVideoGameRepository
	{
		void SaveAsync();
		Task<VideoGame> GetSingleAsync(object id);
		Task<IEnumerable<VideoGame>> GetAllAsync();
		void InsertAsync(VideoGame game);
		void UpdateAsync(VideoGame game);
		void DeleteAsync(object id);
	}
}
