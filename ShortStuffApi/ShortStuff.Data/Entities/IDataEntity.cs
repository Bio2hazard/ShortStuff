// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataEntity.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The DataEntity interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Entities
{
    /// <summary>
    /// The DataEntity interface.
    /// </summary>
    /// <typeparam name="TId">
    /// </typeparam>
    public interface IDataEntity<TId>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        TId Id { get; set; }
    }
}