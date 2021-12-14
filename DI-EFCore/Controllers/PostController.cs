using Microsoft.AspNetCore.Mvc;

using DI_EFCore.Entities;
using DI_EFCore.Interfaces.Repositories;

namespace DI_EFCore.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase {

        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts() {
            return Ok(await _repository.GetAllPosts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post?>> GetPost(int id) {
            var post = await _repository.GetPost(id);

            if (post != null) {
                return Ok(post);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(Post post) {
            var createdPost = await _repository.AddPost(post);

            return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, Post post) {

            if (await _repository.GetPost(id) != null) {
                await _repository.UpdatePost(id, post);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id) {
            var post = await _repository.GetPost(id);

            if (post != null) {
                await _repository.DeletePost(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}