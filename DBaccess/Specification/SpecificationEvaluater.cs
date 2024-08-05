
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DBaccess.Specification
{
    //It trasforms the specification obejct to LINQ query 
    public class SpecificationEvaluater<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec){
            var query = inputQuery;
            if(spec.Criteria != null){
                query = query.Where(spec.Criteria);
            }
            if(spec.OrderBy != null){
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDesc != null){
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            if(spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (current,include)=>current.Include(include));
            return query;
        }
    }
}