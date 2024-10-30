using AutoMapper;
using gRPC.Domain.Entities;
using gRPC.Infrastructure.Protos.product;

namespace gRPC.Service
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ProductEntity, ProductBase>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => Convert.ToSingle(src.UnitPrice)));
            CreateMap<ProductBase, ProductEntity>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.UnitPrice)));
        }
    }
}