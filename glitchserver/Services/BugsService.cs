using System;
using System.Collections.Generic;
using glitchserver.Models;
using glitchserver.Repositories;

namespace glitchserver.Services
{
    public class BugsService
    {
        private readonly BugsRepository _repo;
        public BugsService(BugsRepository repo)
        {
            _repo = repo;
        }

        internal List<Bug> GetAll()
        {
            return _repo.GetAll();
        }

        internal Bug GetOne(int id)
        {
            Bug bug = _repo.GetOne(id);
            if (bug == null)
            {
                throw new Exception("Invalid Id");
            }
            return bug;
        }

        internal bool Delete(int id, Account user)
        {
            Bug bug = _repo.GetOne(id);
            if (bug.CreatorId != user.Id)
            {
                throw new Exception("Invalid Id or You are not the creator");
            }
            return _repo.Delete(id);
        }

        internal Bug Create(Bug newBug)
        {
            return _repo.Create(newBug);
        }

        internal Bug Edit(int id, Bug newBug)
        {
            Bug bug = _repo.GetOne(newBug.Id);
            if (bug == null)
            {
                throw new Exception("Invalid Id");
            }
            if (bug.CreatorId != newBug.CreatorId)
            {
                throw new Exception("You are not the creator of this Bug");
            }
            return _repo.Edit(id, newBug);
        }
    }
}