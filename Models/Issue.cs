using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeetleTracker.Models
{
    public class Issue : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("Reporter")]
        [Required]
        public string Reporter { get; set; }  // string must be replaced by User

        [BsonElement("Assignee")]
        public string Assignee { get; set; }  // string must be replaced by User

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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime Created { get; set; }

        [BsonElement("Updated")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime Updated { get; set; }

        [BsonElement("Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
