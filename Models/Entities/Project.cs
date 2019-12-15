using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeetleTracker.Models.Entities
{
    public class Project : IEntity
    {
        public Project()
        {
            IssueIds = new List<string>();
        }

        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("ProjectLeadId")]
        [Required]
        public Guid ProjectLeadId { get; set; }  

        [BsonElement("Category")]
        public Category Category { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Url")]
        [Url]
        public string Url { get; set; }

        [BsonElement("Issues")]
        public IEnumerable<string> IssueIds { get; set; }
    }
}
