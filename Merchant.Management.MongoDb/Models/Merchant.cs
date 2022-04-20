using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Merchant.Management.MongoDb.Models
{
    public class Merchant
    {
        [BsonId]
        public Guid MerchantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
