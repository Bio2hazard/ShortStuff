// ShortStuff.Repository
// UserRepository.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Data;
using ShortStuff.Data.Entities;

namespace ShortStuff.Repository
{
    public class UserRepository : RepositoryBase<User, Domain.Entities.User, decimal>
    {
        public UserRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
