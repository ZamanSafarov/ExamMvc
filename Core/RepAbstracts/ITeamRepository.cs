using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Core.RepAbstracts
{
    public interface ITeamRepository:IGenericRepository<Team>
    {
        Task<IPagedList<Team>> GetPagedTeamsAsync(int pageIndex, int pageSize);

    }
}
