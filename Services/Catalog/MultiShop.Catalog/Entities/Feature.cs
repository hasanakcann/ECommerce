using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities;

public class Feature
{
    /// <summary>
    ///     MongoDb ilişkisel veritabanı olmadığı için id string tutulur.Guid değer atanır.
    ///     Id olduğunu belirtmek için [BsonId] ile [BsonRepresentation(BsonType.ObjectId)] attribute'leri eklenir.
    ///     [BsonId] id olduğu belirtir.
    ///     [BsonRepresentation(BsonType.ObjectId)] benzersiz olduğunu belirtir.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string FeatureId { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
}
