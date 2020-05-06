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
        private readonly IMongoCollection<Circle> _circle;

        public WallService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _post = database.GetCollection<Post>(settings.PostsCollectionName);
            _circle = database.GetCollection<Circle>(settings.CirclesCollectionName);
        }



        public List<Post> Wall(string userName, string guestName)
        {
            var user = _user.Find(user => user.UserName == userName).FirstOrDefault();
            var guest = _user.Find(guest => guest.UserName == guestName).FirstOrDefault();

            if (user.BlockedUsernames == null)
            {
                user.BlockedUsernames = new List<string>();
            }

            if (!user.BlockedUsernames.Contains(guest.UserName))
            {
                List<Post> myWallPosts = new List<Post>();

                var userposts = _post.Find(post => post.Author == userName).ToList();

                foreach (var posts in userposts)
                {
                    if (posts.CirclePost == false)
                    {
                        myWallPosts.Add(posts);
                    }
                }

                if (user.Circles != null)
                {
                    foreach (var guestcircleid in guest.Circles)
                    {
                        foreach (var usercircleid in user.Circles)
                        {
                            if (guestcircleid == usercircleid)
                            {
                                var usercircles = _circle.Find(c => c.Id == usercircleid).FirstOrDefault();

                                foreach (var id in usercircles.PostsId)
                                {
                                    var uposts = _post.Find(p => p.Id == id).FirstOrDefault();
                                    myWallPosts.Add(uposts);
                                }
                            }
                        }
                    }
                }

                return myWallPosts;
            }

            return null;
        }
    }
}
