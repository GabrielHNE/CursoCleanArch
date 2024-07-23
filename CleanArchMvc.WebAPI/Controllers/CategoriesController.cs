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

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var cat = await _catService.GetById(id);
        
        if(cat == null)
            return NotFound("Category not found");

        return Ok(cat);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryDTO catDTO)
    {
        if(catDTO == null)
            return BadRequest("Invalid data");

        await _catService.Add(catDTO);

        return new CreatedAtRouteResult("GetCategory", new { id = catDTO.Id }, catDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if(id != categoryDTO.Id)
            return BadRequest();

        if(categoryDTO == null)
            return BadRequest();

        await _catService.Update(categoryDTO);

        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Delete(int id)
    {
        var cat = _catService.GetById(id);

        if(cat == null)
            return NotFound("Category not found");

        await _catService.Remove(id);

        return Ok(cat);
    }
}