using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class EchoRepository : RepositoryBase<Data.Entities.Echo, Echo, int>
    {
        public EchoRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
