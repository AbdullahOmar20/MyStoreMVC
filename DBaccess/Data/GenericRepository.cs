
using Core.Entities;
using Core.Interfaces;
using DBaccess.Specification;
using Microsoft.EntityFrameworkCore;


namespace DBaccess.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyShopDbContext _context;
        public GenericRepository(MyShopDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpec( ISpecification<T> spec)
        {
            return SpecificationEvaluater<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}