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

		public async void DeleteAsync(object id)
		{
			VideoGame game = await _context.VideoGames.FindAsync(id);
			_context.VideoGames.Remove(game);
		}


		public async Task<IEnumerable<VideoGame>> GetAllAsync()
		{
			return await _context.VideoGames.ToListAsync();
		}

		public async Task<VideoGame> GetSingleAsync(object id)
		{
			return await _context.VideoGames.FindAsync(id);
		}

		public async void InsertAsync(VideoGame game)
		{
			await _context.VideoGames.AddAsync(game);
		}

		public async void SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async void UpdateAsync(VideoGame game)
		{
			VideoGame oldGame = await _context.VideoGames.FindAsync(game.Id);
			oldGame = game;
			_context.VideoGames.Update(oldGame);
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
