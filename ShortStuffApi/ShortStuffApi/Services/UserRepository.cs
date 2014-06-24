// ShortStuffApi
// UserRepository.cs

using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ShortStuffApi.Interfaces;
using ShortStuffApi.Models;

namespace ShortStuffApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoCollection<User> _users;

        public UserRepository() : this("")
        {
        }

        /// <summary>
        /// Constructor sets up the database connection
        /// </summary>
        /// <param name="connectionString">MongoDB Override</param>
        private UserRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = "mongodb://localhost";
            }

            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("api", WriteConcern.Acknowledged);
            _users = database.GetCollection<User>("users");

            // Test Data
            _users.RemoveAll();
            for (var index = 1; index < 5; index++)
            {
                var user = new User
                {
                    Name = string.Format("test{0}", index),
                    Picture = string.Format("testPicture{0}", index),
                    Tag = string.Format("testString{0}", index)
                };
                CreateUser(user);
            }
        }

        /// <summary>
        /// Returns all Users
        /// </summary>
        /// <returns>List of Users</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return _users.FindAll();
        }

        /// <summary>
        /// Returns specific User
        /// </summary>
        /// <param name="id">ObjectID of the User to fetch</param>
        /// <returns>Single User object</returns>
        public User GetUser(ObjectId id)
        {
            var query = Query<User>.EQ(u => u.Id, id);
            return _users.FindOne(query);
        }

        /// <summary>
        /// Creates a new User in the Database
        /// </summary>
        /// <param name="user">User object to store in the Database</param>
        /// <returns>The stored User object</returns>
        public User CreateUser(User user)
        {
            _users.Insert(user);
            return user;
        }

        /// <summary>
        /// Deletes a User from the Database
        /// </summary>
        /// <param name="id">ObjectId of the User to delete</param>
        /// <returns>True if Deleted, False if User could not be found</returns>
        public bool DeleteUser(ObjectId id)
        {
            var query = Query<User>.EQ(u => u.Id, id);
            var result = _users.Remove(query);
            return result.UpdatedExisting;
        }

        /// <summary>
        /// Updates one or more properties of a User
        /// </summary>
        /// <param name="id">ObjectId of the User to update</param>
        /// <param name="user">Updated User object</param>
        /// <returns>True if Updated, False if User could not be found</returns>
        public bool UpdateUser(ObjectId id, User user)
        {
            var query = Query<User>.EQ(u => u.Id, id);
            var update = Update<User>.Set(u => u.Name, user.Name)
                                     .Set(u => u.Picture, user.Picture)
                                     .Set(u => u.Tag, user.Tag);
            var result = _users.Update(query, update);
            return result.UpdatedExisting;
        }
    }
}
