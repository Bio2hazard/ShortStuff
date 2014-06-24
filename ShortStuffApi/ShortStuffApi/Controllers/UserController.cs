// ShortStuffApi
// UserController.cs

using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MongoDB.Bson;
using ShortStuffApi.Interfaces;
using ShortStuffApi.Models;
using ShortStuffApi.Services;

namespace ShortStuffApi.Controllers
{
    public class UserController : ApiController
    {
        private static readonly IUserRepository Users = new UserRepository();

        /// <summary>
        /// Returns all Users
        /// </summary>
        /// <returns>List of Users</returns>
        public IEnumerable<User> Get()
        {
            return Users.GetAllUsers();
        }

        /// <summary>
        /// Returns specific User
        /// </summary>
        /// <param name="id">String representation of the ObjectID</param>
        /// <returns>Single User object</returns>
        /// <exception cref="HttpResponseException">HTTP Status Not Found is thrown if no user could be found</exception>
        public User Get(string id)
        {
            var user = Users.GetUser(ObjectId.Parse(id));
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        /// <summary>
        /// Creates a new User in the Database
        /// </summary>
        /// <param name="data">User object to store in the Database</param>
        /// <returns>The stored User object</returns>
        public User Post(User data)
        {
            var user = Users.CreateUser(data);
            return user;
        }

        /// <summary>
        /// Updates one or more properties of a User
        /// </summary>
        /// <param name="id">String representation of the ObjectID</param>
        /// <param name="user">Updated User object</param>
        /// <exception cref="HttpResponseException">HTTP Status Not Found is thrown if no user could be found</exception>
        public void Put(string id, User data)
        {
            if (!Users.UpdateUser(ObjectId.Parse(id), data))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Deletes a User from the Database
        /// </summary>
        /// <param name="id">String representation of the ObjectID</param>
        /// <exception cref="HttpResponseException">HTTP Status Not Found is thrown if no user could be found</exception>
        public void Delete(string id)
        {
            if (!Users.DeleteUser(ObjectId.Parse(id)))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
