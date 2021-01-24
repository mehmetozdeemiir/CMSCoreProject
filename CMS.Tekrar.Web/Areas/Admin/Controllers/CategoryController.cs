using CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository) => _categoryRepo = categoryRepository;

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                var slug = await _categoryRepo.FirstOrDefault(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exsist..!");
                    TempData["Warning"] = "The category already exsist..!";
                    return View(category);
                }
                else
                {
                    await _categoryRepo.Add(category);
                    TempData["Success"] = "The category added..!";
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["Error"] = "The category hasn't been added..!";
                return View(category);
            }
        }

        public async Task<IActionResult> List() => View(await _categoryRepo.Get(x => x.Status != Status.Passive));

        public async Task<IActionResult> Edit(int id) => View(await _categoryRepo.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                var slug = _categoryRepo.FirstOrDefault(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "There is already a category..!");
                    TempData["Warning"] = "The page  is already exsist..!";
                    return View(category);
                }
                else
                {
                    category.UpdateDate = DateTime.Now;
                    category.Status = Status.Modified;
                    await _categoryRepo.Update(category);
                    TempData["Success"] = "The category has been edited..!";
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["Error"] = "The category hasn't been edited..!";
                return View(category);
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            Category category = await _categoryRepo.GetById(id);
            if (category != null)
            {
                await _categoryRepo.Delete(category);
                TempData["Success"] = "The category deleted..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The category hasn't been deleted..!";
                return RedirectToAction("List");
            }
        }

    }
}
