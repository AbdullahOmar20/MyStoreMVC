using Core.Interfaces;
using System.Linq.Expressions;

namespace DBaccess.Specification
{
    //A design pattern that transform queries to objects
    //mainly used to not to expose the implementation of our queries while using Generic Repository pattern 
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria=criteria;
        }
        public BaseSpecification()
        {
            
        }
        //type of data structures(Expression tree) that represent lambda expression
        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T,object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;} 

        public Expression<Func<T, object>> OrderByDesc {get; private set;}

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        //for eager loading
        protected void AddInclude(Expression<Func<T, object>> includeExp)
        {
            Includes.Add(includeExp);
        }
        protected void AddOrderBy(Expression<Func<T,object>> order)
        {
            OrderBy = order;
        }
        protected void AddOrderByDESC(Expression<Func<T,object>> order)
        {
            OrderByDesc = order;
        }
        protected void ApplyPaging(int take, int skip)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }
    }
}