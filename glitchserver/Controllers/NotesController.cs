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
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesService _service;

        public NotesController(NotesService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Note>> Create([FromBody] Note newNote)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                newNote.CreatorId = user.Id;
                _service.Create(newNote);
                newNote.Creator = user;
                return Ok(newNote);

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Note>> Delete(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                bool Deleted = _service.Delete(id, user);
                if (Deleted)
                {
                    return Ok("Successfully Deleted");
                }
                else
                {
                    throw new System.Exception("Invalid Id");
                }
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}