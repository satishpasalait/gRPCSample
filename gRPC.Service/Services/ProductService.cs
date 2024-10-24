using Grpc.Core;
using gRPC.Infrastructure.Protos.product;


namespace gRPC.Service.Services;

public class ProductService : Product.ProductBase
{ 
    public ProductService() { }

    public override async Task<ProductResponse> ReadProduct(ProductRequest request, ServerCallContext context)
    {
        if (request == null || request.Id <= 0 ) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter a valid request product data."));

        return await Task.FromResult(new ProductResponse
        {
            Id = 1,
            ProductName = "Fan"
        });
    }
}
