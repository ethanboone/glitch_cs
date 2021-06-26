using System.Collections.Generic;
using System.Threading.Tasks;
using glitchserver.Services;
using glitchserver.Models;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;

namespace glitchserver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugsController : ControllerBase
    {
        private readonly BugsService _service;
        private readonly NotesService _notesService;

        public BugsController(BugsService service, NotesService notesService)
        {
            _service = service;
            _notesService = notesService;
        }

        [HttpGet]
        public ActionResult<List<Bug>> GetAllBugs()
        {
            try
            {
                List<Bug> bugs = _service.GetAll();
                return Ok(bugs);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Bug> GetOne([FromBody] int id)
        {
            try
            {
                Bug bug = _service.GetOne(id);
                return Ok(id);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Bug>> Delete(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                bool deleted = _service.Delete(id, user);
                if (deleted)
                {
                    return Ok("Successfully Deleted");
                }
                else
                {
                    throw new System.Exception("Invalid/Incorrect Id");
                }
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Bug>> Edit(int id, [FromBody] Bug newBug)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                newBug.Id = id;
                newBug.CreatorId = user.Id;
                Bug resBug = _service.Edit(id, newBug);
                return Ok(resBug);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Bug>> Create([FromBody] Bug newBug)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                newBug.CreatorId = user.Id;
                Bug resBug = _service.Create(newBug);
                return Ok(resBug);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/notes")]
        public ActionResult<List<Note>> GetAllNotes(int id)
        {
            try
            {
                List<Note> notes = _notesService.GetAllNotes(id);
                return Ok(notes);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}