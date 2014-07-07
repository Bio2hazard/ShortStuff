using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class UserRepository : RepositoryBase<Data.Entities.User, User, decimal>
    {
        public UserRepository(ShortStuffContext context)
            : base(context)
        {
        }
    }
}
