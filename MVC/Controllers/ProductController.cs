using System.Diagnostics;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using DBaccess.Specification;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class ProductController : Controller
{
    
    private readonly IUnitofWork _unitofwork;
    private readonly IGenericRepository<Product> _repo;
    private readonly IMapper _mapper;
    public ProductController(IUnitofWork unitofwork, IGenericRepository<Product> repo, IMapper mapper)
    {
        _unitofwork=unitofwork;
        _repo=repo;
        _mapper=mapper;
    }
    [HttpGet("/")]
    public async Task<IActionResult> Index([FromQuery] ProductParams productParams)
    {
        var repo = _unitofwork.Repository<Product>();
        var spec = new ProductWithBrandAndTypeSpecification(productParams);
        var result = await repo.ListWithSpecAsync(spec);
        var mapping = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(result);

        return View(new Pagination<ProductToReturnDTO>(productParams.PageNumber, productParams.PageSize, mapping));
    }


}
