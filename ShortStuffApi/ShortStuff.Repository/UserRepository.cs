// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The user repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
    using ShortStuff.Data;
    using ShortStuff.Data.Entities;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : RepositoryBase<User, Domain.Entities.User, decimal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public UserRepository(ShortStuffContext context)
            : base(context)
        {
        }
    }
}