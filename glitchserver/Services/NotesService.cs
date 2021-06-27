using System;
using System.Collections.Generic;
using glitchserver.Models;
using glitchserver.Repositories;

namespace glitchserver.Services
{
    public class NotesService
    {
        private readonly NotesRepository _repo;

        public NotesService(NotesRepository repo)
        {
            _repo = repo;
        }

        internal List<Note> GetAllNotes(int id)
        {
            throw new NotImplementedException();
        }

        internal void Create(Note newNote)
        {
            throw new NotImplementedException();
        }

        internal bool Delete(int id, Account user)
        {
            Note note = _repo.GetOne(id);
            if (note.CreatorId != user.Id)
            {
                throw new Exception("You are not the creator of this Note");
            }
            return _repo.Delete(id);
        }
    }
}