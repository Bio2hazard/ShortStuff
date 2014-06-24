// ShortStuffApi
// IUserRepository.cs

using System.Collections.Generic;
using MongoDB.Bson;
using ShortStuffApi.Models;

namespace ShortStuffApi.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Returns all Users
        /// </summary>
        /// <returns>List of Users</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Returns specific User
        /// </summary>
        /// <param name="id">ObjectID of the User to fetch</param>
        /// <returns>Single User object</returns>
        User GetUser(ObjectId id);

        /// <summary>
        /// Creates a new User in the Database
        /// </summary>
        /// <param name="user">User object to store in the Database</param>
        /// <returns>The stored User object</returns>
        User CreateUser(User user);

        /// <summary>
        /// Deletes a User from the Database
        /// </summary>
        /// <param name="id">ObjectId of the User to delete</param>
        /// <returns>True if Deleted, False if User could not be found</returns>
        bool DeleteUser(ObjectId id);

        /// <summary>
        /// Updates one or more properties of a User
        /// </summary>
        /// <param name="id">ObjectId of the User to update</param>
        /// <param name="user">Updated User object</param>
        /// <returns>True if Updated, False if User could not be found</returns>
        bool UpdateUser(ObjectId id, User user);
    }
}
