
namespace Infrastructure.Services
{
    
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T> Data { get; set; }   
        public Pagination(int pageindex, int pageSize, IReadOnlyList<T> data) 
        {
            PageIndex=pageindex;
            PageSize=pageSize;
            Data=data;
        }
    }
}