using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Handin3.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public bool Login { get; set; }
        public string Password { get; set; }
        public List<string> BlockedUsernames { get; set; }
        public List<string> PostsId { get; set; }
        public List<string> Circles { get; set; }
        public List<User> FollowUser { get; set; }
    }
}



