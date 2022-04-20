using AutoMapper;

namespace Merchant.Management.Mappers
{
    public class MerchantMapperProfile : Profile
    {
        public MerchantMapperProfile()
        {
            CreateMap<Models.CreateMerchant, MongoDb.Models.Merchant>();
            CreateMap<Models.Merchant, MongoDb.Models.Merchant>().ReverseMap();
            CreateMap<MongoDb.Models.Merchant, Messaging.Publisher.Events.MerchantCreatedEvent>();
        }
    }
}
