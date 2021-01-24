using CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Entity.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository productRepository,ICategoryRepository categoryRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepository;
            _categoryRepo = categoryRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryRepo.Get(x => x.Status != Status.Passive), "Id", "Name");
            return View();
            //ViewBag=> Controller da oluşturulan bir yapıyı View kısmına taşımak için kullanılır.
            //SelectList=>Dropdown(web formsda combobox benzeri) List in içini doldurmak için kullanılır. 
        }

        [HttpPost]

        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                string imageName = "noimage.png";
                if (product.ImageUpload!=null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "/meia/product");
                    imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();
                
                }
                product.Image = imageName;
                await _productRepo.Add(product);              
                TempData["Success"] = "Product Eklendi..!"; 
                return View();
            }
            else
            {
                TempData["Error"] = "The product hasn't been add..!";
                return View(product);
            }
        }
        public async Task<IActionResult> List() => View(await _productRepo.Get(x => x.Status != Status.Passive));
        
        public async Task<IActionResult>Edit(int id)
        {
            Product product = await _productRepo.GetById(id);

            ViewBag.CategoryId = new SelectList (await _categoryRepo.Get(x => x.Status != Status.Passive), "Id", "Name", product.CategoryId);

            return View(product);
        }
      [HttpPost]

      public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");

                    if (!string.Equals(product.Image, "noimage.png"))
                    {
                        string oldPath = Path.Combine(uploadDir, product.Image);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();
                    product.Image = imageName;


                }
                await _productRepo.Update(product);
                TempData["Success"] = "The product edited..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The product hasnt been edited..!";
                return View(product);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productRepo.GetById(id);
            if (product!=null)
            {
                await _productRepo.Delete(product);
                TempData["Success"] = "The product deleted..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The product hast been deleted.!";
                return RedirectToAction("List");
            }
        }

    }
}
