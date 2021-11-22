using Microsoft.EntityFrameworkCore;
using RetroClient.Data;
using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	public class VideoGamePlatformRepository : IVideoGamePlatformRepository, IDisposable
	{

		private ApplicationDbContext _context;
		private bool disposed = false;

		public VideoGamePlatformRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Delete(object id)
		{
			var platform = _context.Platforms.Find(id);
			_context.Platforms.Remove(platform);
		}

		public IEnumerable<VideoGamePlatform> GetAll()
		{
			return _context.Platforms.ToList();
		}

		public VideoGamePlatform GetSingle(object id)
		{
			return _context.Platforms.Find(id);
		}

		public void Insert(VideoGamePlatform platform)
		{
			_context.Platforms.Add(platform);
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Update(VideoGamePlatform platform)
		{
			_context.Entry(platform).State = EntityState.Modified;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
