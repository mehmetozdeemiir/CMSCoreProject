using CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.Get(x=>x.Status!=Status.Passive));
        }

        public async Task<IActionResult> ProductByCategory(string categorySlug)
        {
            Category category= await _categoryRepository.FirstOrDefault(x => x.Slug == categorySlug);

            if (category == null) return RedirectToAction("Index");

            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;
            List<Product> products = await _productRepository.Get(x => x.CategoryId == category.Id);
            return View(products);
        
        
        }

    }
}
