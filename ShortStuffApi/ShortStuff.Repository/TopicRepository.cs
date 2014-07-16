// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicRepository.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The topic repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
    using ShortStuff.Data;
    using ShortStuff.Data.Entities;

    /// <summary>
    /// The topic repository.
    /// </summary>
    public class TopicRepository : RepositoryBase<Topic, Domain.Entities.Topic, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopicRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public TopicRepository(ShortStuffContext context)
            : base(context)
        {
        }
    }
}