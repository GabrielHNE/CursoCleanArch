using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _catService;
    public CategoriesController(ICategoryService catService)
    {
        _catService = catService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _catService.GetCategories();
        if(categories == null)
        {
            return NotFound("Categories not found.");
        }

        return Ok(categories);
    }
}