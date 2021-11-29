using RetroClient.Data;
using RetroClient.Models;
using RetroClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Services
{
	public class SettingService
	{
		private ApplicationDbContext _context;
		private ISettingRepository _settingRepo;

		public SettingService(ApplicationDbContext context)
		{
			_context = context;
			_settingRepo = new SettingRepository(_context);
		}

		public async Task InsertSetting(Setting newSetting)
		{
			if (newSetting is null)
			{
				throw new Exception("Entity cannot be null");
			}

			await _settingRepo.InsertAsync(newSetting);
			await _settingRepo.SaveAsync();
		}

		public async Task<IEnumerable<Setting>> ListSettings()
		{
			return await _settingRepo.GetAllAsync();
		}

		public async Task<Setting> GetSetting(object id)
		{
			if (id is null)
			{
				throw new Exception("id cannot be null");
			}

			return await _settingRepo.GetSingleAsync(id) ?? null;
		}

		public async Task DeleteSetting(object id)
		{
			if (id is null)
			{
				throw new Exception("id cannot be null");
			}

			await _settingRepo.DeleteAsync(id);
			await _settingRepo.SaveAsync();
		}

		public async Task UpdateSetting(Setting setting)
		{

			if (setting is null)
			{
				throw new Exception("Entity cannot be null");
			}

			await _settingRepo.UpdateAsync(setting);
			await _settingRepo.SaveAsync();
		}

	}
}
