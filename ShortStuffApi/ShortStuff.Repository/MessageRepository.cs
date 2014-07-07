using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class MessageRepository : RepositoryBase<Data.Entities.Message, Message, int>
    {
        public MessageRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
