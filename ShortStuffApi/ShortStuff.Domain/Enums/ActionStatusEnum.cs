// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionStatusEnum.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Enumerable listing the possible Status of an ActionResult.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Enums
{
    /// <summary>
    /// Enumerable listing the possible Status of an ActionResult.
    /// </summary>
    public enum ActionStatusEnum
    {
        /// <summary>
        /// Denotes that the service completed successfully.
        /// </summary>
        Success, 

        /// <summary>
        /// Denotes that an exception occured and was caught by the service.
        /// </summary>
        ExceptionError, 

        /// <summary>
        /// Denotes that one or more validation rules were broken.
        /// </summary>
        ValidationError, 

        /// <summary>
        /// Denotes that the service was unable to locate the requested data.
        /// </summary>
        NotFound, 

        /// <summary>
        /// Denotes that a conflict between unique keys occured.
        /// </summary>
        Conflict
    }
}