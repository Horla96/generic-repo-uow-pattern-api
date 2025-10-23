﻿using generic_repo_pattern_api.Entity;
using generic_repo_pattern_api.Repository;
using generic_repo_pattern_api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace generic_repo_pattern_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithGenericRepoController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        public ProductWithGenericRepoController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest product)
        {
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price
            };
            var createdProduct = await _productRepository.AddAsync(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            await _productRepository.UpdateAsync(existingProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(existingProduct);
            return NoContent();
        }

    }
}
