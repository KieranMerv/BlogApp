using dotnet_BlogApp.Data.Repositories;
using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Models.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_BlogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Read: Get All Public Posts

        // Read: GET All Posts of Current User
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _unitOfWork.PostRepo.GetAll();

            if (posts == null) return NotFound();

            return Ok(posts);
        }

        // Read: GET Post [Complete]
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById([FromRoute] Guid id)
        {
            if ((await _unitOfWork.PostRepo.GetAll()) == null) return NotFound();

            var post = await _unitOfWork.PostRepo.GetById(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // Create: POST New Post [Complete]
        [HttpPost]
        public async Task<ActionResult<string>> CreatePost([FromBody] PostAddEditVM postAddEditVM)
        {
            if ((await _unitOfWork.PostRepo.GetAll()) == null) return Problem("Post Repository is null. Please contact administrator.");

            var post = new Post()
            {
                Title = postAddEditVM.Title,
                Body = postAddEditVM.Body,
                Created = postAddEditVM.Created,
                Updated = postAddEditVM.Updated,
                IsPrivate = postAddEditVM.IsPrivate
            };

            post.Id = Guid.NewGuid();

            await _unitOfWork.PostRepo.Add(post);

            var saveAsyncInt = await _unitOfWork.SaveAsync();

            if (saveAsyncInt <= 0) return BadRequest("An error occurred. Changes were not saved.");

            return CreatedAtAction(nameof(GetPostById), new {id = post.Id}, post.Id);
        }

        // Update: PUT Existing Post [Complete]
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdatePost([FromBody] PostAddEditVM postAddEditVM, [FromRoute] Guid id)
        {
            if ((await _unitOfWork.PostRepo.GetAll()) == null) return Problem("Post Repository is null. Please contact administrator.");

            var post = await _unitOfWork.PostRepo.GetById(id);

            if (post == null) return NotFound();

            _unitOfWork.PostRepo.Update(postAddEditVM, post);

            var saveAsyncInt = await _unitOfWork.SaveAsync();

            if (saveAsyncInt <= 0) return BadRequest("An error occurred. Changes were not saved.");

            return Ok("Post updated.");
        }

        // Delete: DELETE Existing Post [Complete]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePost([FromRoute] Guid id)
        {
            if ((await _unitOfWork.PostRepo.GetAll()) == null) return NotFound();

            var post = await _unitOfWork.PostRepo.GetById(id);

            if (post == null) return NotFound();

            _unitOfWork.PostRepo.Delete(post);

            var saveAsyncInt = await _unitOfWork.SaveAsync();

            if (saveAsyncInt <= 0) return BadRequest("An error occurred. Changes were not saved.");

            return Ok("Post deleted.");
        }
    }
}