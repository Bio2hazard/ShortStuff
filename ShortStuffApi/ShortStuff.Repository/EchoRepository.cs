// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EchoRepository.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The echo repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
    using ShortStuff.Data;
    using ShortStuff.Data.Entities;

    /// <summary>
    /// The echo repository.
    /// </summary>
    public class EchoRepository : RepositoryBase<Echo, Domain.Entities.Echo, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EchoRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public EchoRepository(ShortStuffContext context)
            : base(context)
        {
        }
    }
}