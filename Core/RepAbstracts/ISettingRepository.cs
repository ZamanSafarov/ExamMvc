using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Core.RepAbstracts
{
    public interface ISettingRepository:IGenericRepository<Setting>
    {
        Task<IPagedList<Setting>> GetPagedSettingsAsync(int pageIndex, int pageSize);
    }
}
