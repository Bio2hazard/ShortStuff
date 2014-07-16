// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionSubStatusEnum.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Enumerable listing the possible SubStatus of an ActionResult.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Enums
{
    /// <summary>
    /// Enumerable listing the possible SubStatus of an ActionResult.
    /// </summary>
    public enum ActionSubStatusEnum
    {
        /// <summary>
        /// No specific SubStatus applies to this ActionResult.
        /// </summary>
        None, 

        /// <summary>
        /// This SubStatus denotes that a new element was created by the service.
        /// </summary>
        Created, 

        /// <summary>
        /// This SubStatus denotes that the service changed one or more values of an element.
        /// </summary>
        Updated, 

        /// <summary>
        /// This SubStatus denotes that the service tried to change one or more values of an element, 
        /// but the values matched the ones persisted in the database, resulting in nothing being changed.
        /// </summary>
        NoChange
    }
}