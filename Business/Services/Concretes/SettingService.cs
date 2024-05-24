using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Concretes
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _repository;
        public SettingService(ISettingRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateSettingAsync(Setting setting)
        {
            await _repository.AddEntityAsync(setting);
            await _repository.CommitAsync();
        }

        public void DeleteSetting(int id)
        {
            var setting = _repository.GetEntity(x => x.DeletedDate == null && x.Id == id);
            if (setting is null)
            {
                throw new EntityNullException("Setting Not Found!");
            }
            _repository.Commit();
        }

        public List<Setting> GetAllSetting(Func<Setting, bool>? func = null)
        {
            return _repository.GetAllEntity(func);
        }

        public Task<IPagedList<Setting>> GetPagedSettingsAsync(int pageIndex, int pageSize)
        {
            return _repository.GetPagedSettingsAsync(pageIndex, pageSize);
        }

        public Setting GetSetting(Func<Setting, bool>? func = null)
        {
            return _repository.GetEntity(func);
        }

        public Task<Dictionary<string, string>> GetSettingsAsync()
        {
            return _repository.GetSettingsAsync();
        }

        public void UpdateSetting(int id, Setting setting)
        {
            var oldSetting = _repository.GetEntity(x => x.DeletedDate == null && x.Id == id);
            if (oldSetting is null)
            {
                throw new EntityNullException("Setting Not Found!");
            }
            oldSetting.Key = setting.Key;
            oldSetting.Value = setting.Value;

            _repository.Commit();
        }
    }
}
