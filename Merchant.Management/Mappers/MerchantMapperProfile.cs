using AutoMapper;

namespace Merchant.Management.Mappers
{
    public class MerchantMapperProfile : Profile
    {
        public MerchantMapperProfile()
        {
            CreateMap<Models.CreateMerchant, MongoDb.Models.Merchant>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.MerchantId, opt => opt.Ignore());

            CreateMap<Models.Merchant, MongoDb.Models.Merchant>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<MongoDb.Models.Merchant, Messaging.Publisher.Events.MerchantCreatedEvent>()
                .ForMember(dest => dest.EventName, opt => opt.Ignore());
        }
    }
}
