using gRPC.Domain.Entities;

namespace gRPC.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();

        Task<ProductEntity?> GetProductByIdAsync(int productId);

        Task<int> CreateProductAsync(ProductEntity product);

        Task<int> UpdateProductAsync(ProductEntity product);

        Task<int> DeleteProductAsync(ProductEntity product);
    }
}