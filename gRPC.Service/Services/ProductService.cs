﻿using Grpc.Core;
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
        if (request == null || request.Id <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter valid product data."));

        var product = await _productRepository.GetProductByIdAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Product was not found."));

        var responseProduct = _mapper.Map<ProductBase>(product);

        return await Task.FromResult(new ReadProductResponse
        {
            Product = responseProduct
        });
    }

    public override async Task<ListProductResponse> ListProduct(ListProductRequest request, ServerCallContext context)
    {
        var response = new ListProductResponse();
        var products = await _productRepository.GetAllAsync();

        foreach (var product in products)
        {
            response.Products.Add(new ProductBase
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
        return await Task.FromResult(response);
    }

    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        if (request.Product == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter valid product data."));

        var result = 0;

        var createProduct = _mapper.Map<ProductEntity>(request.Product);

        result = await _productRepository.CreateProductAsync(createProduct);

        return await Task.FromResult(new CreateProductResponse
        {
            Id = result
        });
    }

    public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
    {
        if (request.Product == null || request.Product.Id <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter valid product data."));

        var updateProduct = await _productRepository.GetProductByIdAsync(request.Product.Id);

        var result = 0;

        if (updateProduct != null)
        {
            updateProduct.ProductName = request.Product.ProductName;
            updateProduct.ProductDescription = request.Product.ProductDescription;
            updateProduct.ProductType = request.Product.ProductType;
            updateProduct.Quantity = request.Product.Quantity;
            updateProduct.UnitPrice = Convert.ToDecimal(request.Product.UnitPrice);
            updateProduct.Vendor = request.Product.Vendor;

            result = await _productRepository.UpdateProductAsync(updateProduct);
        }

        return await Task.FromResult(new UpdateProductResponse
        {
            Id = result
        });
    }

    public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        if (request.Id <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter valid product data."));

        var deleteProduct = await _productRepository.GetProductByIdAsync(request.Id);

        var result = 0;

        if (deleteProduct != null)
        {
            result = await _productRepository.DeleteProductAsync(deleteProduct);
        }

        return await Task.FromResult(new DeleteProductResponse
        {
            Id = result
        });
    }
}