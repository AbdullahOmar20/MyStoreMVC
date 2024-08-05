

namespace DBaccess.Specification
{

    //the object where we will take data from the query params to pass to the specificatio classes
    public class ProductParams
    {
        private const int maxsize = 50;
        public int PageNumber{get;set;} = 1;
        private int pageSize=10;
        public int PageSize{
            get => pageSize;
            set => pageSize = (value<maxsize) ? value : maxsize;
        }
        public int? BrandId {get;set;} 
        public int? TypeId { get; set; }
        public string sort {get; set; }
        private string search;
        public string Search { get=>search; set=> search=value; }
    }
}