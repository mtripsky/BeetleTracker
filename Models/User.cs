using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeetleTracker.Models
{
    public class User : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserName")]
        [Required]
        public string UserName { get; set; }

        [BsonElement("FullName")]
        public string FullName { get; set; }

        [BsonElement("Email")]
        [Required, EmailAddress]
        public string Email { get; set; }

        [BsonElement("Password")]
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        //[BsonElement("AvatarUrl")]
        //[Display(Name = "Avatar")]
        //[DataType(DataType.ImageUrl)]
        //public string AvatarUrl { get; set; }
    }
}