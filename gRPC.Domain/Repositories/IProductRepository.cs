using gRPC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
