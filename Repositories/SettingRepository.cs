using RetroClient.Data;
using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	public class SettingRepository : ISettingRepository, IDisposable
	{
		private ApplicationDbContext _context;
		private bool disposed = false;

		public SettingRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task DeleteAsync(object id)
		{
			Setting setting = await _context.UserSettings.FindAsync(id);
			_context.UserSettings.Remove(setting);
		}


		public async Task<IEnumerable<Setting>> GetAllAsync()
		{
			return _context.UserSettings.ToList();
		}

		public async Task<Setting> GetSingleAsync(object id)
		{
			return await _context.UserSettings.FindAsync(id) ?? null;
		}

		public async Task InsertAsync(Setting setting)
		{
			await _context.UserSettings.AddAsync(setting);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Setting setting)
		{
			Setting oldSetting = await _context.UserSettings.FindAsync(setting.Id);
			oldSetting = setting;
			_context.UserSettings.Update(oldSetting);
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
