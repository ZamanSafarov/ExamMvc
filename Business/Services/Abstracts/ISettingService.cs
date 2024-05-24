using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Abstracts
{
    public interface ISettingService
    {
        Task CreateSettingAsync(Setting setting);
        void DeleteSetting(int id);
        void UpdateSetting(int id,Setting setting);
        Setting GetSetting(Func<Setting, bool>? func = null);
        List<Setting> GetAllSetting(Func<Setting, bool>? func = null);
        Task<IPagedList<Setting>> GetPagedSettingsAsync(int pageIndex, int pageSize);
        Task<Dictionary<string, string>> GetSettingsAsync();
    }
}
