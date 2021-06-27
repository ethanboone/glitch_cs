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
            string sql = @"
            SELECT 
                b.*,
                a.*
            FROM bugs b
            JOIN accounts a ON a.id = b.creatorId
            WHERE 
                b.id = @id
            LIMIT 1;
            ";
            return _db.Query<Bug, Account, Bug>(sql, (b, a) =>
            {
                b.Creator = a;
                return b;
            },
            new { id }
            ).FirstOrDefault();
        }

        internal bool Delete(int id)
        {
            string sql = @"
            DELETE FROM bugs WHERE id = @id LIMIT 1;
            ";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows == 1;
        }

        internal Bug Edit(int id, Bug newBug)
        {
            string sql = @"
            UPDATE bugs
            SET 
                closed = @Closed,
                description = @Description,
                title = @Title,
                closedDate = @ClosedDate,
                creatorId = @CreatorId
            WHERE 
                id = @id
            ";
            _db.Execute(sql, newBug);
            return newBug;
        }

        internal Bug Create(Bug newBug)
        {
            string sql = @"
            INSERT INTO 
            bugs (closed, description, title, creatorId)
            VALUES (@Closed, @Description, @Title, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            newBug.Id = _db.ExecuteScalar<int>(sql, newBug);
            return newBug;
        }
    }
}