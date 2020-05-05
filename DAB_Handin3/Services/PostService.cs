using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using MongoDB.Driver;

namespace DAB_Handin3.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _post;
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Circle> _circle;

        public PostService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _post = database.GetCollection<Post>(settings.PostsCollectionName);
            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _circle = database.GetCollection<Circle>(settings.CirclesCollectionName);
        }

        public List<Post> Get() => _post.Find(post => true).ToList();

        public List<Post> Get(string author) => _post.Find(post => post.Author == author).ToList();

        public Post CreatePost(Post post)
        {
            var user = _user.Find(user => user.UserName == post.Author).FirstOrDefault();

            if (user.PostsId == null)
            {
                user.PostsId = new List<string>();
            }
            _post.InsertOne(post);
            user.PostsId.Add(post.Id);
            _user.ReplaceOne(user => user.UserName == post.Author, user);

            return post;
        }


        public void Update(string id, Comment comment)
        {
            var newPost = _post.Find(post => post.Id == id).FirstOrDefault();

            newPost.Comments.Add(comment);
            _post.ReplaceOne(post => post.Id == id, newPost);
        }


        public void Remove(string id)
        {
            var post = _post.Find(post => post.Id == id).FirstOrDefault();
            var user = _user.Find(user => user.UserName == post.Author).FirstOrDefault();

            if (user.PostsId == null)
            {
                user.PostsId = new List<string>();
            }

            user.PostsId.Remove(post.Id);
            _user.ReplaceOne(user => user.UserName == post.Author, user);
            _post.DeleteOne(post => post.Id == id);
        }


        public void create_circle_post(string userName, string circlename, Post post)
        {
            var cirle = _circle.Find(circle => circle.CircleName == circlename).FirstOrDefault();
            var user = _user.Find(user => user.UserName == post.Author).FirstOrDefault();

            if (cirle.PostsId == null)
            {
                cirle.PostsId = new List<string>();
            }

          
            if (user.PostsId == null)
            {
                user.PostsId = new List<string>();
            }

            _post.InsertOne(post);
            user.PostsId.Add(post.Id);
            _user.ReplaceOne(user => user.UserName == post.Author, user);

            cirle.PostsId.Add(post.Id);
            _circle.ReplaceOne(circle => circle.CircleName == circlename, cirle);
                
            
        }
    }
}

