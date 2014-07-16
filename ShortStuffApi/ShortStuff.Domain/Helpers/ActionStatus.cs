// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionStatus.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The action status helper class provides a Status, a SubStatus and a Id to let service consumers know whether their request succeeded or failed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Helpers
{
    using ShortStuff.Domain.Enums;

    /// <summary>
    /// The action status helper class provides a Status, a SubStatus and a Id to let service consumers know whether their request succeeded or failed.
    /// </summary>
    /// <typeparam name="TId">
    /// The type of the id.
    /// </typeparam>
    public class ActionStatus<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionStatus{TId}"/> class.
        /// Status defaults to Success, while SubStatus defaults to None.
        /// </summary>
        public ActionStatus()
        {
            Status = ActionStatusEnum.Success;
            SubStatus = ActionSubStatusEnum.None;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// The Status provides information on whether the Action succeeded, or failed via <see cref="ActionStatusEnum"/>. And if it failed, why it failed.
        /// </summary>
        public ActionStatusEnum Status { get; set; }

        /// <summary>
        /// Gets or sets the SubStatus.
        /// The SubStatus provides additional information about the success of an Action via <see cref="ActionSubStatusEnum"/>. 
        /// </summary>
        public ActionSubStatusEnum SubStatus { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// The Id is a unique identifier of a element, and is populated during the creation of new elements by the service.
        /// </summary>
        public TId Id { get; set; }
    }
}