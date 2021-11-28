using RetroClient.Data;
using RetroClient.Models;
using RetroClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Services
{
	public class VideoGameService
	{

		private readonly ApplicationDbContext _context;		
		private IVideoGameRepository _gameRepository;

		public VideoGameService(ApplicationDbContext context)
		{
			_context = context;
			_gameRepository = new VideoGameRepository(_context);
		}

		public async Task InsertGame(VideoGame newGame)
		{
			if (newGame is null)
			{
				throw new Exception("Entity cannot be null");
			}

			await _gameRepository.InsertAsync(newGame);
			await _gameRepository.SaveAsync();
		}

		public async Task<IEnumerable<VideoGame>> ListGames()
		{
			return await _gameRepository.GetAllAsync();
		}

		public async Task<VideoGame> GetGame(object id)
		{
			if (id is null)
			{
				throw new Exception("id cannot be null");
			}

			return await _gameRepository.GetSingleAsync(id) ?? null;
		}

		public async Task DeleteGame(object id)
		{
			if (id is null)
			{
				throw new Exception("id cannot be null");
			}

			await _gameRepository.DeleteAsync(id);
			await _gameRepository.SaveAsync();
		}

		public async Task UpdateGame(VideoGame game)
		{

			if (game is null)
			{
				throw new Exception("Entity cannot be null");
			}

			await _gameRepository.UpdateAsync(game);
			await _gameRepository.SaveAsync();
		}
	}
}
