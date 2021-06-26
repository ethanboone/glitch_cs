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

        public BugsController(BugsService service)
        {
            _service = service;
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
    }
}