// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Echo.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The echo.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Entities
{
    using System;

    /// <summary>
    /// The echo.
    /// </summary>
    public class Echo : IDataEntity<int>
    {
        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Gets or sets the creator id.
        /// </summary>
        public decimal CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the source message.
        /// </summary>
        public virtual Message SourceMessage { get; set; }

        /// <summary>
        /// Gets or sets the source message id.
        /// </summary>
        public int SourceMessageId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }
}