using System.Collections;
using Core.Interfaces;
using DBaccess;
using DBaccess.Data;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly MyShopDbContext _context;
        private Hashtable _repos;
        public UnitOfWork(MyShopDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if(_repos == null)
            {
                _repos = new Hashtable();
            }
            var type = typeof(T).Name;
            if(!_repos.ContainsKey(type))
            {
                var repotype = typeof(GenericRepository<>);
                var repoInstance = Activator.CreateInstance(repotype.MakeGenericType(typeof(T)), _context);
                _repos.Add(type, repoInstance);
            }
            return (IGenericRepository<T>)_repos[type];
        }

        public async Task<int> saving()
        {
            return await _context.SaveChangesAsync();
        }
        private bool disposed = false;
        private void dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            dispose(true);

        }
    }


}