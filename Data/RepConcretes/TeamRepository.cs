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
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly AppDbContext _appDbContext;
        public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<IPagedList<Team>> GetPagedTeamsAsync(int pageIndex, int pageSize)
        {
           var query = _appDbContext.Teams.Where(x=>x.DeletedDate==null).AsQueryable();
            return query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
