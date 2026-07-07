using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopifyApi.DTOs.Products;
using ShopifyApi.Interfaces;


namespace ShopifyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // constructor injection
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //GET api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        //GET api/products/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        //POST api/products
        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create(CreateProductDto dto)
        {
            var product = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT api/products/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDto>> Update(int id, UpdateProductDto dto)
        {
            var product = await _productService.UpdateAsync(id, dto);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // DELETE api/products/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.DeleteAsync(id);
            if (!product) return NotFound();
            return NoContent();
        }
    }
}
