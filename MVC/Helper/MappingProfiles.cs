
using AutoMapper;
using Core.Entities;
using MVC.Models;

namespace MVC.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDTO>()
            .ForMember(d=>d.ProductBrandName, o=>o.MapFrom(s=>s.ProductBrand.Name))
            .ForMember(d=>d.ProductTypeName, o=>o.MapFrom(s=>s.ProductType.Name));
            
        }
    }
}