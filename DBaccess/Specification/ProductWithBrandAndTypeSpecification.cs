
using Core.Entities;

namespace DBaccess.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductParams productParams) : base(criteria => 
        (String.IsNullOrEmpty(productParams.Search) || criteria.Name.ToLower().Contains(productParams.Search.ToLower())) &&
        (!productParams.BrandId.HasValue || criteria.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || criteria.ProductTypeId == productParams.TypeId) 
        )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            ApplyPaging(productParams.PageSize, productParams.PageSize*(productParams.PageNumber - 1));
            if(!String.IsNullOrEmpty(productParams.sort))
            {
                switch(productParams.sort)
                {
                    case "PriceASC":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "PriceDESC":
                        AddOrderByDESC(x=>x.Price);
                        break;
                    default:
                        AddOrderBy(x=>x.Name);
                        break;
                }               
            }

        }
        public ProductWithBrandAndTypeSpecification(int id): base(x=> x.Id==id)
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}