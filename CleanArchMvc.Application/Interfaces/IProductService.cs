using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();

        Task<ProductDTO> GetById(int? id);
        // Task<ProductDTO> GetProductCategory(int? id);
        Task Add(ProductDTO prodDto);
        Task Update(ProductDTO prodDTO);
        Task Remove(int? id);
    }
}