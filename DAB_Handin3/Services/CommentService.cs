﻿using System;
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

        public void Update(string id, Comment commentIn)
        {
            var post = _post.Find(post => post.Id == id).FirstOrDefault();

            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }

            post.Comments.Add(commentIn);
            _post.ReplaceOne(post => post.Id == id, post);
        }
    }
}

