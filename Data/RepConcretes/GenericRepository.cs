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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddEntityAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public int Commit()
        {
            return _appDbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public void DeleteEntity(T entity)
        {
            entity.DeletedDate = DateTime.UtcNow.AddHours(4);
            _appDbContext.Set<T>().Update(entity);
        }

        public List<T> GetAllEntity(Func<T, bool>? func = null)
        {
            return func==null ? _appDbContext.Set<T>().ToList() : _appDbContext.Set<T>().Where(func).ToList();
        }

        public T GetEntity(Func<T, bool>? func = null)
        {
            return func == null ? _appDbContext.Set<T>().FirstOrDefault() : _appDbContext.Set<T>().FirstOrDefault(func);
        }
    }
}
