using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class TopicRepository : RepositoryBase<Data.Entities.Topic, Topic, int>
    {
        public TopicRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
