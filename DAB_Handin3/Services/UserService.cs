using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using MongoDB.Driver;

namespace DAB_Handin3.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() =>
        _user.Find(user => true).ToList();

        public User Get(string userName) =>
            _user.Find(user => user.UserName == userName).FirstOrDefault();

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Update(string userName, User userIn) =>
            _user.ReplaceOne(user => user.UserName == userName, userIn);

        public void Remove(User userIn) =>
            _user.DeleteOne(user => user.UserName == userIn.UserName);

        public void Remove(string userName) =>
            _user.DeleteOne(user => user.UserName == userName);



        public void follow_user(string userName, string follow)
        {
            var user = _user.Find(user => user.UserName == userName).FirstOrDefault();
            var userfollow = _user.Find(user => user.UserName == follow).FirstOrDefault();

            if (user.FollowUser == null)
            {
                user.FollowUser = new List<User>();
            }

            userfollow.FollowUser = null;

            user.FollowUser.Add(userfollow);
            _user.ReplaceOne(user => user.UserName == userName, user);
        }

    }
}

