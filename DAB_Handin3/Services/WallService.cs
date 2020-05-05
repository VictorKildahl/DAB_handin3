using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using MongoDB.Driver;

namespace DAB_Handin3.Services
{
    public class WallService
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Post> _post;

        public WallService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _post = database.GetCollection<Post>(settings.PostsCollectionName);
        }



        public List<Post> Wall(string userName, string guestName)
        {
            var user = _user.Find(user => user.UserName == userName).FirstOrDefault();

            List<Post> myWallPosts = new List<Post>();

            if (user.BlockedUsernames == null)
            {
                user.BlockedUsernames = new List<string>();
            }

            if (!user.BlockedUsernames.Contains(guestName))
            {
                foreach (var id in user.PostsId)
                {
                    myWallPosts.Add(_post.Find(p => p.Id == id).FirstOrDefault());
                }

                return myWallPosts;
            }
            return null;
        }
    }
}
