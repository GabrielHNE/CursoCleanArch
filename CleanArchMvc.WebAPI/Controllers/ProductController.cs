using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var prods = await _productService.GetProducts();

            if(prods == null)
                return NotFound("Produtos não encontrados");

            return Ok(prods);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get(int id)
        {
            var prod = await _productService.GetById(id);

            if(prod == null)
                return NotFound("Product not fund");
            
            return Ok(prod);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if(productDTO == null)
                return BadRequest();

            await _productService.Add(productDTO);
            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            if(id != productDTO.Id)
            return BadRequest();

            if(productDTO == null)
                return BadRequest();

            await _productService.Update(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var prod = _productService.GetById(id);

            if(prod == null)
                return NotFound("Product not found");

            await _productService.Remove(id);

            return Ok(prod);
        }
    }
}