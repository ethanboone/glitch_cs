using System.Data;
using glitchserver.Models;
using Dapper;
using System.Collections.Generic;
using System;
using System.Linq;

namespace glitchserver.Repositories
{
    public class AccountsRepository
    {
        private readonly System.Data.IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }

        internal Account Create(Account newAccount)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newAccount);
            return newAccount;
        }
    }
}