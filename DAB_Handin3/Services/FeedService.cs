﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;

namespace DAB_Handin3.Services
{
    public class FeedService
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Post> _post;
        private readonly IMongoCollection<Circle> _circle;

        public FeedService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _post = database.GetCollection<Post>(settings.PostsCollectionName);
            _circle = database.GetCollection<Circle>(settings.CirclesCollectionName);
        }


        public List<Post> Feed(string logged_in_userName)
        {
            var user = _user.Find(user => user.UserName == logged_in_userName).FirstOrDefault();

            List<Post> myFeedPosts = new List<Post>();

            if (user.Login == true)
            {
                var test = _post.Find(post => post.Author == logged_in_userName).ToList();

                foreach (var posts in test)
                {
                    myFeedPosts.Add(posts);
                }
                
                if (user.FollowUser.Count != 0)
                {
                    foreach (var person in user.FollowUser)
                    {
                        if (person.PostsId == null)
                        {
                            person.PostsId = new List<string>();
                        }

                        foreach (var id in person.PostsId)
                        {
                            var followerPosts = _post.Find(p => p.Id == id).FirstOrDefault();

                            if (followerPosts.CirclePost == false)
                            {
                                myFeedPosts.Add(followerPosts);
                            }
                        }
                    }
                }

                if (user.Circles != null)
                {
                    foreach (var circle in user.Circles)
                    {
                        var myCircles = _circle.Find(c => c.Id == circle).FirstOrDefault();

                        foreach (var id in myCircles.PostsId)
                        {
                            var post = _post.Find(p => p.Id == id).FirstOrDefault();
                            myFeedPosts.Add(post);
                        }
                    }
                }
            }

            return myFeedPosts;
        }
    }
}
