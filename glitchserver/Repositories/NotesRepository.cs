using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using glitchserver.Models;

namespace glitchserver.Repositories
{
    public class NotesRepository
    {
        private readonly IDbConnection _db;

        public NotesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Note GetOne(int id)
        {
            string sql = @"
            SELECT 
                n.*,
                a.*
            FROM notes n
            JOIN accounts a ON a.id = n.creatorId
            WHERE
                n.id = @id;
            ";
            return _db.Query<Note, Account, Note>(sql, (n, a) =>
            {
                n.Creator = a;
                return n;
            }, new { id }).FirstOrDefault();
        }

        internal bool Delete(int id)
        {
            string sql = @"
            DELETE FROM notes WHERE id = @id LIMIT 1;
            ";
            int AffectedRows = _db.Execute(sql, new { id });
            return AffectedRows == 1;
        }

        internal List<Note> GetAll(int id)
        {
            string sql = @"
            SELECT
                n.*,
                a.*
            FROM notes n
            JOIN accounts a ON n.creatorId = a.id
            WHERE
                n.bugId = @id;
            ";
            return _db.Query<Note, Account, Note>(sql, (n, a) =>
            {
                n.Creator = a;
                return n;
            }, new { id }).ToList();
        }

        internal Note Create(Note newNote)
        {
            string sql = @"
            INSERT INTO 
            notes (body, bugId, creatorId)
            VALUES (@Body, @BugId, @CreatorId);
            SELECT LAST_INSERT_ID;
            ";
            newNote.Id = _db.ExecuteScalar<int>(sql, newNote);
            return newNote;
        }
    }
}