using Grpc.Core;
using gRPC.Infrastructure.Protos.product;
using gRPC.Domain.Repositories;


namespace gRPC.Service.Services;

public class ProductService : Product.ProductBase
{ 
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository) 
    { 
        _productRepository = productRepository;
    }

    public override async Task<ProductResponse> ReadProduct(ProductRequest request, ServerCallContext context)
    {
        if (request == null || request.Id <= 0 ) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter a valid request product data."));

        var product = await _productRepository.GetProductByIdAsync(request.Id) 
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Product was not found."));

        return await Task.FromResult(new ProductResponse
        {
            Id = product.Id,
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            ProductType = product.ProductType,
            Quantity = product.Quantity,
            UnitPrice = Convert.ToSingle(product.UnitPrice),
            Vendor = product.Vendor,
        });
    }
}
