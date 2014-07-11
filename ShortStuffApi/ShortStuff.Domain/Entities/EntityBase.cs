// ShortStuff.Domain
// EntityBase.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

namespace ShortStuff.Domain.Entities
{
    public abstract class EntityBase<TId> : ValidatableBase
    {
        public TId Id { get; set; }
    }
}
