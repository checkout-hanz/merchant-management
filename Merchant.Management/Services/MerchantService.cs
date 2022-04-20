using AutoMapper;
using Merchant.Management.Messaging.Publisher;
using Merchant.Management.Messaging.Publisher.Events;
using Merchant.Management.MongoDb.Repositories;
using Merchant.Management.Utils;

namespace Merchant.Management.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPublisher<MerchantCreatedEvent> _publisher;

        public MerchantService(IMerchantRepository merchantRepository, IMapper mapper, IDateTimeProvider dateTimeProvider, IPublisher<MerchantCreatedEvent> publisher)
        {
            _merchantRepository = merchantRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _publisher = publisher;
        }

        public async Task<IEnumerable<Models.Merchant>> GetMerchants()
        {
            var merchants = await _merchantRepository.GetMerchants();
            return _mapper.Map<IEnumerable<Models.Merchant>>(merchants);
        }

        public async Task<Models.Merchant> GetMerchant(string id)
        {
            if (!Guid.TryParse(id, out var merchantId))
            {
                return null;
            }

            var merchant = await _merchantRepository.GetMerchant(merchantId);
            if (merchant == null)
            {
                return null;
            }

            return _mapper.Map<Models.Merchant>(merchant);
        }

        public async Task<Guid> AddMerchant(Models.CreateMerchant merchant)
        {
            // validation

            // mapping
            var merchantModel = _mapper.Map<MongoDb.Models.Merchant>(merchant);
            var merchantId = Guid.NewGuid();
            merchantModel.MerchantId = merchantId;
            merchantModel.CreatedDate = _dateTimeProvider.UtcNow;

            // Add Merchant
            await _merchantRepository.InsertMerchant(merchantModel);

            // publish merchant created event
            var merchantCreatedEvent = _mapper.Map<MerchantCreatedEvent>(merchantModel);
            await _publisher.PublishAsync(merchantCreatedEvent);

            return merchantId;
        }
    }
}
