using Grpc.Core;
using gRPC.Infrastructure.Protos.product;
using gRPC.Domain.Repositories;
using AutoMapper;
using gRPC.Domain.Entities;


namespace gRPC.Service.Services;

public class ProductService : Product.ProductBase
{ 
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper) 
    { 
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public override async Task<ReadProductResponse> ReadProduct(ReadProductRequest request, ServerCallContext context)
    {
        if (request == null || request.Id <= 0 ) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter valid product data."));

        var product = await _productRepository.GetProductByIdAsync(request.Id) 
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Product was not found."));

        var responseProduct = _mapper.Map<ReadProductResponse>(product);

        return await Task.FromResult(new ReadProductResponse
        {
            Product = responseProduct.Product
        });
    }

    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        if (request.Product == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter valid product data."));

        var createProduct = _mapper.Map<ProductEntity>(request.Product);

        var result = await _productRepository.CreateProductAsync(createProduct);

        return await Task.FromResult(new CreateProductResponse
        { 
            Id = result
        });
    }
}
