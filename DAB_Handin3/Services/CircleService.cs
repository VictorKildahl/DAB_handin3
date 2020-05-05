using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using MongoDB.Driver;

namespace DAB_Handin3.Services
{
    public class CircleService
    {
        private readonly IMongoCollection<Circle> _circle;
        private readonly IMongoCollection<User> _user;

        public CircleService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _circle = database.GetCollection<Circle>(settings.CirclesCollectionName);
            _user = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<Circle> Get() =>
            _circle.Find(circle => true).ToList();

        public List<Circle> Get(string circleName) =>
            _circle.Find(circle => circle.CircleName == circleName).ToList();

        public Circle Create(Circle circle)
        {
            _circle.InsertOne(circle);
            return circle;
        }

        //public void Update(string groupName, Circle circleIn) =>
        //    _circle.ReplaceOne(circle => circle.CircleName == groupName, circleIn);


        public void Update(string id, User user)
        {
            var newCircle = _circle.Find(circle => circle.Id == id).FirstOrDefault();

            if (user.Circles == null)
            {
                user.Circles = new List<string>();
            }

            user.Circles.Add(newCircle.Id);
            _user.ReplaceOne(user1 => user1.Id == user.Id, user);

            if (newCircle.Users == null)
            {
                newCircle.Users = new List<string>();
            }

            newCircle.Users.Add(user.Id);
            _circle.ReplaceOne(circle => circle.Id == id, newCircle);

            
        }

        public void Remove(string id) =>
            _circle.DeleteOne(circle => circle.Id == id);
    }
}
