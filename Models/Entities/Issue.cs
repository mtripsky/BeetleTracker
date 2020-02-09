using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeetleTracker.Models.Entities
{
    public class Issue : IEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("ReporterId")]
        [Required]
        public Guid ReporterId { get; set; }  

        [BsonElement("AssigneeId")]
        public Guid AssigneeId { get; set; }  

        [BsonElement("Type")]
        [Required]
        public IssueType Type { get; set; }

        [BsonElement("Priority")]
        [Required]
        public IssuePriority Priority { get; set; }

        [BsonElement("Status")]
        public IssueStatus Status { get; set; }

        [BsonElement("Created")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [BsonElement("Updated")]
        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; }

        [BsonElement("Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [BsonElement("Project")]
        public string ProjectId { get; set; }
    }
}
