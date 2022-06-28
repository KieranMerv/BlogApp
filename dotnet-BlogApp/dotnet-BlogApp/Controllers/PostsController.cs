using dotnet_BlogApp.Data.Repositories;
using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnet_BlogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public PostsController(
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // Read: Get All Public Posts
        [HttpGet("public")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPublicPosts()
        {
            var posts = await _unitOfWork.PostRepo.GetAllPublicPosts();

            if (posts == null) return NotFound();

            return Ok(posts);
        }

        // Read: GET All Posts of Current User
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetUserPosts()
        {
            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            var posts = await _unitOfWork.PostRepo.GetAllUserPosts(currentUser.Id);

            if (posts == null) return NotFound();

            return Ok(posts);
        }

        // Read: GET Post [Complete]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById([FromRoute] Guid id)
        {
            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if ((await _unitOfWork.PostRepo.GetAllUserPosts(currentUser.Id)) == null) return NotFound();

            var post = await _unitOfWork.PostRepo.GetById(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        // Create: POST New Post [Complete]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> CreatePost([FromBody] PostAddEditVM postAddEditVM)
        {
            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if ((await _unitOfWork.PostRepo.GetAllUserPosts(currentUser.Id)) == null) return Problem("Post Repository is null. Please contact administrator.");

            var post = new Post()
            {
                Title = postAddEditVM.Title,
                Body = postAddEditVM.Body,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsPrivate = postAddEditVM.IsPrivate,
                AppUserId = currentUser.Id
            };

            post.Id = Guid.NewGuid();

            await _unitOfWork.PostRepo.Add(post);

            var saveAsyncInt = await _unitOfWork.SaveAsync();

            if (saveAsyncInt <= 0) return BadRequest("An error occurred. Changes were not saved.");

            return CreatedAtAction(nameof(GetPostById), new {id = post.Id}, post.Id);
        }

        // Update: PUT Existing Post [Complete]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdatePost([FromBody] PostAddEditVM postAddEditVM, [FromRoute] Guid id)
        {
            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if ((await _unitOfWork.PostRepo.GetAllUserPosts(currentUser.Id)) == null) return Problem("Post Repository is null. Please contact administrator.");

            var post = await _unitOfWork.PostRepo.GetById(id);

            if (post == null) return NotFound();

            _unitOfWork.PostRepo.Update(postAddEditVM, post);

            var saveAsyncInt = await _unitOfWork.SaveAsync();

            if (saveAsyncInt <= 0) return BadRequest("An error occurred. Changes were not saved.");

            return Accepted();
        }

        // Delete: DELETE Existing Post [Complete]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePost([FromRoute] Guid id)
        {
            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currentUser = await _userManager.FindByEmailAsync(currentUserEmail);

            if ((await _unitOfWork.PostRepo.GetAllUserPosts(currentUser.Id)) == null) return NotFound();

            var post = await _unitOfWork.PostRepo.GetById(id);

            if (post == null) return NotFound();

            _unitOfWork.PostRepo.Delete(post);

            var saveAsyncInt = await _unitOfWork.SaveAsync();

            if (saveAsyncInt <= 0) return BadRequest("An error occurred. Changes were not saved.");

            return Accepted();
        }
    }
}