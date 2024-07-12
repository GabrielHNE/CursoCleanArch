using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _catService;
        public CategoriesController(ICategoryService catService)
        {
            _catService = catService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _catService.GetCategories();
            return View(categories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet()]
        public IActionResult Create(){
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CategoryDTO cat){
            if(ModelState.IsValid)
            {
                await _catService.Add(cat);
                return RedirectToAction(nameof(Index));
            }

            return View(cat);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id){
            if(id == null) return NotFound();

            var catDTO = await _catService.GetById(id);
            return catDTO != null? View(catDTO) : NotFound();
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO cat){
            if(ModelState.IsValid) {
                try {
                    await _catService.Update(cat);
                }
                catch(Exception ex) {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(cat);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id){
            if(id == null) return NotFound();

            var catDTO = await _catService.GetById(id);
            return catDTO != null? View(catDTO) : NotFound();
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _catService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id){
            if(id == null)
                return NotFound();

            var catDTO = await _catService.GetById(id);

            if(catDTO == null)
                return NotFound();

            return View(catDTO);
        }
    }
}