using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	interface IVideoGameRepository
	{
		void Save();
		VideoGame GetSingle(object id);
		IEnumerable<VideoGame> GetAll();
		void Insert(VideoGame game);
		void Update(VideoGame game);
		void Delete(object id);
	}
}
