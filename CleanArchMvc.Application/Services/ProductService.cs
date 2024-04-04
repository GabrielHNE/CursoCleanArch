using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interface;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Add(ProductDTO prodDto)
        {
            var ProductCreateCommand = _mapper.Map<ProductCreateCommand>(prodDto);

            await _mediator.Send(ProductCreateCommand);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if(productByIdQuery == null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        // public async Task<ProductDTO> GetProductCateroty(int? id)
        // {
        //     var productByIdQuery = new GetProductByIdQuery(id.Value);

        //     if(productByIdQuery == null)
        //         throw new Exception($"Entity could not be loaded");

        //     var result = await _mediator.Send(productByIdQuery);

        //     return _mapper.Map<ProductDTO>(result);
        // }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsQuery = new GetProductsQuery();

            if(productsQuery == null) throw new Exception("Entity could not be loaded.");

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);

            if(productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");
            
            await _mediator.Send(productRemoveCommand);
        }

        public async Task Update(ProductDTO prodDTO)
        {
            var ProductCreateCommand = _mapper.Map<ProductUpdateCommand>(prodDTO);

            await _mediator.Send(ProductCreateCommand);
        }
    }
}