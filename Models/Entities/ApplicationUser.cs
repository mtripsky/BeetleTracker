using AspNetCore.Identity.MongoDbCore.Models;

namespace BeetleTracker.Models.Entities
{
    public class ApplicationUser : MongoIdentityUser, IEntity
    {
    }
}