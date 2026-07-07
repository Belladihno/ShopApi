using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopifyApi.Data;
using ShopifyApi.DTOs.Products;
using ShopifyApi.Interfaces;
using ShopifyApi.Mappings;

namespace ShopifyApi.Services
{
    public class ProductService: IProductService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductService(
            AppDbContext context, 
            IValidator<CreateProductDto> createValidator,
            IValidator<UpdateProductDto> updateValidator
            )
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;

        }
        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            return await _context.Products.Select(p => p.ToResponseDto()).ToListAsync();
        }
        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product?.ToResponseDto(); // ?. returns null if not found
        }
        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            var product = dto.ToModel();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ToResponseDto();
        }
        public async Task<ProductResponseDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);

            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.ApplyUpdate(dto);

            await _context.SaveChangesAsync();
            return product.ToResponseDto();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
