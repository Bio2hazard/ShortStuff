// ShortStuff.Repository
// MessageRepository.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Data;
using ShortStuff.Data.Entities;

namespace ShortStuff.Repository
{
    public class MessageRepository : RepositoryBase<Message, Domain.Entities.Message, int>
    {
        public MessageRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
