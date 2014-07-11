// ShortStuff.Data
// IDataEntity.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

namespace ShortStuff.Data.Entities
{
    public interface IDataEntity<TId>
    {
        TId Id { get; set; }
    }
}
