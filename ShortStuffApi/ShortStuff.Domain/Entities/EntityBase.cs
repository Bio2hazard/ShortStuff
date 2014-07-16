// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The base which all business models must inherit.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    /// <summary>
    /// The base which all business models must inherit.
    /// </summary>
    /// <typeparam name="TId">
    /// Type of the unique identifier of the model.
    /// </typeparam>
    public abstract class EntityBase<TId> : ValidatableBase
    {
        /// <summary>
        /// Gets or sets the Id that uniquely identifies this model or it's underlying data equivalent.
        /// </summary>
        public TId Id { get; set; }
    }
}