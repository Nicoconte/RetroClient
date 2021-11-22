using Microsoft.EntityFrameworkCore;
using RetroClient.Data;
using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	public class VideoGameRepository : IVideoGameRepository, IDisposable
	{
		private ApplicationDbContext _context;
		private bool disposed = false;

		public VideoGameRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Delete(object id)
		{
			VideoGame game = _context.VideoGames.Find(id);
			_context.VideoGames.Remove(game);
		}


		public IEnumerable<VideoGame> GetAll()
		{
			return _context.VideoGames.ToList();
		}

		public VideoGame GetSingle(object id)
		{
			return _context.VideoGames.Find(id);
		}

		public void Insert(VideoGame game)
		{
			_context.VideoGames.Add(game);
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Update(VideoGame game)
		{
			_context.Entry(game).State = EntityState.Modified; 
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
