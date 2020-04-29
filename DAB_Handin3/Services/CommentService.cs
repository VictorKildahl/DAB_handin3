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

        public Comment Get(string id) => _comment.Find(comment => comment.Id == id).FirstOrDefault();

        public void Update(string id, Comment commentIn) => _comment.ReplaceOne(comment => comment.Id == id, commentIn);
    }
}

