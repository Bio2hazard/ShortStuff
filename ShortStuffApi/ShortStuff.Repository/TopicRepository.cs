// ShortStuff.Repository
// TopicRepository.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Data;
using ShortStuff.Data.Entities;

namespace ShortStuff.Repository
{
    public class TopicRepository : RepositoryBase<Topic, Domain.Entities.Topic, int>
    {
        public TopicRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
