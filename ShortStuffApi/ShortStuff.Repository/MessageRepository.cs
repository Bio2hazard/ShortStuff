// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageRepository.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The message repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
    using ShortStuff.Data;
    using ShortStuff.Data.Entities;

    /// <summary>
    /// The message repository.
    /// </summary>
    public class MessageRepository : RepositoryBase<Message, Domain.Entities.Message, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public MessageRepository(ShortStuffContext context)
            : base(context)
        {
        }
    }
}