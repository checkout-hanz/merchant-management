namespace Merchant.Management.Services
{
    public interface IMerchantService
    {
        Task<Guid> AddMerchant(Models.CreateMerchant merchant);

        Task<IEnumerable<Models.Merchant>> GetMerchants();

        Task<Models.Merchant> GetMerchant(Guid merchantId);
    }
}
