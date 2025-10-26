using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockOne.API.Repository;

namespace StockOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository repository;

        public CommentController(ICommentRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("getAllComments")]
        public async  Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await repository.GetAllCommentsAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getCommentById/{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                var comment = await repository.GetCommentByIdAsync(id);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComment([FromBody] Model.DTOs.CommentDTO comment)
        {
            try
            {
                await repository.AddCommentAsync(comment);
                return Ok("Comment added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("updateComment/{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Model.DTOs.CommentDTO comment)
        {
            try
            {
                await repository.UpdateCommentAsync(id, comment);
                return Ok("Comment updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("deleteComment/{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await repository.DeleteCommentAsync(id);
                return Ok("Comment deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
