// ShortStuff.Domain
// CreateStatus.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain.Helpers
{
    public class CreateStatus<TId>
    {
        public CreateStatusEnum Status { get; set; }
        public TId Id { get; set; }
    }
}
