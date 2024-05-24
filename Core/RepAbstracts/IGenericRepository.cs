using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Core.RepAbstracts
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        Task AddEntityAsync(T entity);
        void DeleteEntity(T entity);
        int Commit();
        Task<int> CommitAsync();
        T GetEntity(Func<T,bool>? func =null);
        List<T> GetAllEntity(Func<T,bool>? func =null);
  
    }
}
