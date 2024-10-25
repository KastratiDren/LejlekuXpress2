using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LejlekuXpress.Models
{
    public class Log
    {
        [BsonId]  // Indicates this property is the ID for MongoDB
        [BsonRepresentation(BsonType.ObjectId)]  // Use ObjectId representation
        public ObjectId Id { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
    }

}
