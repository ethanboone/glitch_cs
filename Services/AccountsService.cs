
namespace glitchserver.Services
{
    class AccountsService
    {
        private readonly AccountsRepository _repo;

        public AccountsService(AccountsRepository repo)
        {
            _repo = repo;
        }
    }
}