using AutoMapper;
using gRPC.Domain.Entities;
using gRPC.Infrastructure.Protos.product;



namespace gRPC.Service.AutoMapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        { 
            CreateMap<ProductEntity, ReadProductResponse>().ReverseMap();
        }
    }
}
