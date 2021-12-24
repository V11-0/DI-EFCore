using Microsoft.AspNetCore.Mvc;

using DI_EFCore.Entities;
using DI_EFCore.Interfaces.Repositories;

namespace DI_EFCore.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository) {
            _repository = repository;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() {
            return Ok(await _repository.GetAllUsers());
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(int id) {
            var user = await _repository.GetUser(id);

            if (user != null) {
                return Ok(user);
            }

            return NotFound();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user) {

            if (user.Username == "") {
                return BadRequest("Username can not be empty");
            }

            var createdUser = await _repository.AddUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id) {
            var user = await _repository.GetUser(id);

            if (user != null) {
                await _repository.DeleteUser(user);
                return NoContent();
            }

            return NotFound();
        }
    }
}
