using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Repositories
{
	public interface ISettingRepository
	{
		Task SaveAsync();
		Task<Setting> GetSingleAsync(object id);
		Task<IEnumerable<Setting>> GetAllAsync();
		Task InsertAsync(Setting setting);
		Task UpdateAsync(Setting setting);
		Task DeleteAsync(object id);
	}
}
