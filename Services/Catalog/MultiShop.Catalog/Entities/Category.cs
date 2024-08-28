using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Category
    {
        /// <summary>
        ///     MongoDB ilişkisel veritabanı olmadığı için id string tutulur.Guid değer atanır.
        ///     Id olduğunu belirtmek için [BsonId] ile [BsonRepresentation(BsonType.ObjectId)] attribute'leri eklenir.
        ///     [BsonId] id olduğu belirtir.
        ///     [BsonRepresentation(BsonType.ObjectId)] benzersiz olduğunu belirtir.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
