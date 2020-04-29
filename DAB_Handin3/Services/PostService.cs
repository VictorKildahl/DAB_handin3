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

        public PostService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _post = database.GetCollection<Post>(settings.PostsCollectionName);
        }

        public List<Post> Get() => _post.Find(post => true).ToList();

        public List<Post> Get(string author) => _post.Find(post => post.Author == author).ToList();

        public Post CreatePost(Post post)
        {
            _post.InsertOne(post);
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
            _post.DeleteOne(post => post.Id == id);
        }
    }
}

