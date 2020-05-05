using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using MongoDB.Driver;

namespace DAB_Handin3.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _comment;
        private readonly IMongoCollection<Post> _post;

        public CommentService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _post = database.GetCollection<Post>(settings.PostsCollectionName);
            _comment = database.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        public Comment Get(string id) => _comment.Find(comment => comment.Id == id).FirstOrDefault();

        public void Update(string id, Comment commentIn)// =>_comment.ReplaceOne(comment => comment.Id == id, commentIn);
        {
            var post = _post.Find(post => post.Id == id).FirstOrDefault();

            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }

            _comment.ReplaceOne(comment => comment.Id == id, commentIn);
        }

}
}

