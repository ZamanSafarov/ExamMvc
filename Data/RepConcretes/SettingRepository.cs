using Core.Models;
using Core.RepAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.RepConcretes
{
    public class SettingRepository:GenericRepository<Setting>, ISettingRepository
    {
        private readonly AppDbContext _appDbContext;
        public SettingRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<IPagedList<Setting>> GetPagedSettingsAsync(int pageIndex, int pageSize)
        {
            var query = _appDbContext.Settings.Where(x => x.DeletedDate == null).AsQueryable();
            return query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
