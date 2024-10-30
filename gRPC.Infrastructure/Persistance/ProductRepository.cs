using gRPC.Domain.Entities;
using gRPC.Domain.Repositories;
using gRPC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Infrastructure.Persistance
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<ProductEntity?> GetProductByIdAsync(int productId)
        {
            return await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<int> CreateProductAsync(ProductEntity product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> UpdateProductAsync(ProductEntity product)
        {
            _appDbContext.Products.Update(product);
            await _appDbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> DeleteProductAsync(ProductEntity product)
        {
            _appDbContext.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}