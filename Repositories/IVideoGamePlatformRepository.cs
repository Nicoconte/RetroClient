using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	interface IVideoGamePlatformRepository
	{
		void Save();
		VideoGamePlatform GetSingle(object id);
		IEnumerable<VideoGamePlatform> GetAll();
		void Insert(VideoGamePlatform platform);
		void Update(VideoGamePlatform platform);
		void Delete(object id);
	}
}
