// ShortStuff.Repository
// EchoRepository.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Data;
using ShortStuff.Data.Entities;

namespace ShortStuff.Repository
{
    public class EchoRepository : RepositoryBase<Echo, Domain.Entities.Echo, int>
    {
        public EchoRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
