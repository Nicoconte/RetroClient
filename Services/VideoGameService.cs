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

		public async void InsertGame(VideoGame newGame)
		{
			if (newGame is null)
			{
				throw new Exception("Entity cannot be null");
			}

			_gameRepository.InsertAsync(newGame);
			_gameRepository.SaveAsync();
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

			return await _gameRepository.GetSingleAsync(id);
		}

		public async void DeleteGame(object id)
		{
			if (id is null)
			{
				throw new Exception("id cannot be null");
			}

			_gameRepository.DeleteAsync(id);
			_gameRepository.SaveAsync();
		}

		public async void UpdateGame(VideoGame game)
		{

			if (game is null)
			{
				throw new Exception("Entity cannot be null");
			}

			_gameRepository.UpdateAsync(game);
			_gameRepository.SaveAsync();
		}
	}
}
