using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _prodService;
        private readonly ICategoryService _catService;
        private readonly IWebHostEnvironment _env;
        public ProductsController(IWebHostEnvironment env, IProductService prodService, ICategoryService catService)
        {
            _env = env;
            _prodService = prodService;
            _catService = catService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _prodService.GetProducts();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create(){
            ViewBag.Categories = new 
                SelectList(await _catService.GetCategories(), "Id", "Name");
            
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ProductDTO prod){
            Console.WriteLine(prod.CategoryId);
            if(ModelState.IsValid){
                await _prodService.Add(prod);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new 
                SelectList(await _catService.GetCategories(), "Id", "Name");
            return View(prod);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id){
            if(id == null) return NotFound();
            var productDTO = await _prodService.GetById(id);

            if(productDTO == null) return NotFound();

            var cat = await _catService.GetCategories();
            ViewBag.Categories = new SelectList(cat, "Id", "Name", productDTO.CategoryId);

            return productDTO != null? View(productDTO) : NotFound();
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO prod){
            if(ModelState.IsValid) {
                try {
                    await _prodService.Update(prod);
                }
                catch(Exception ex) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(prod);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id){
            if(id == null) return NotFound();

            var prodDTO = await _prodService.GetById(id);
            return prodDTO != null? View(prodDTO) : NotFound();
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _prodService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int? id){
            if(id == null)
                return NotFound();
            var prodDTO = await _prodService.GetById(id);

            if(prodDTO == null)
                return NotFound();

            var wwwroot = _env.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + prodDTO.Image);
            
            Console.WriteLine(image);

            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(prodDTO);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}