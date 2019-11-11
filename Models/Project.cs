using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeetleTracker.Models
{
    public class Project : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("ProjectLead")]
        [Required]
        public string ProjectLead { get; set; }

        [BsonElement("Category")]
        public Category Category { get; set; }

        [BsonElement("Url")]
        [Url]
        public string Url { get; set; }
    }
}
