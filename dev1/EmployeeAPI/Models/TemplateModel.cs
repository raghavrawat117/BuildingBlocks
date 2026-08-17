using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeAPI.Models
{
    public class TemplateModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("templateName")]
        public string TemplateName { get; set; } = string.Empty;

        [BsonElement("templateContent")]
        public string TemplateContent { get; set; } = string.Empty;

    }
}
