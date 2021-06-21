
using System.Data;

namespace glitchserver.Repositories
{
    class AccountsRepository
    {
        private readonly System.Data.IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}