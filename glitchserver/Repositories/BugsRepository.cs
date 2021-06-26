using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using glitchserver.Models;

namespace glitchserver.Repositories
{
    public class BugsRepository
    {
        private readonly IDbConnection _db;
        public BugsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Bug> GetAll()
        {
            string sql = @"
            SELECT
                b.*.
                a.*
            FROM bugs b
            JOIN accounts a ON a.id = b.creatorId;
            ";
            List<Bug> bugs = _db.Query<Bug, Account, Bug>(sql, (b, a) =>
            {
                b.Creator = a;
                return b;
            }, splitOn: "id").ToList();
            return bugs;
        }

        internal Bug GetOne(int id)
        {
            throw new NotImplementedException();
        }

        internal bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        internal Bug Edit(int id, Bug newBug)
        {
            throw new NotImplementedException();
        }

        internal Bug Create(Bug newBug)
        {
            throw new NotImplementedException();
        }
    }
}