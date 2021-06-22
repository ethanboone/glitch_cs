
using System;
using System.Collections.Generic;
using System.Linq;
using glitchserver.Models;
using glitchserver.Repositories;

namespace glitchserver.Services
{
    public class AccountsService
    {
        private readonly AccountsRepository _repo;
        public AccountsService(AccountsRepository repo)
        {
            _repo = repo;
        }
        internal Account GetOrCreateAccount(Account userInfo)
        {
            Account account = _repo.GetById(userInfo.Id);
            if (account == null)
            {
                return _repo.Create(userInfo);
            }
            return account;
        }

        internal Account GetAccountById(string id)
        {
            return _repo.GetById(id);
        }
    }
}