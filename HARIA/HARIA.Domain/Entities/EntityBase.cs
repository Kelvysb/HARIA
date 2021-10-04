using MongoDB.Bson.Serialization.Attributes;

namespace HARIA.Domain.Entities
{
    public class EntityBase
    {
        [BsonId]
        public string Id { get; set; }
    }
}