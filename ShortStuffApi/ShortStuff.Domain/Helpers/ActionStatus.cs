// ShortStuff.Domain
// ActionStatus.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain.Helpers
{
    public class ActionStatus<TId>
    {
        public ActionStatus()
        {
            Status = ActionStatusEnum.Success;
            SubStatus = ActionSubStatusEnum.None;
        }

        public ActionStatusEnum Status { get; set; }
        public ActionSubStatusEnum SubStatus { get; set; }
        public TId Id { get; set; }
    }
}
