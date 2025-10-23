using StockOne.API.Model;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task AddCommentAsync(CommentDTO stock);
        Task DeleteCommentAsync(int Id);
        Task UpdateCommentAsync(int Id, CommentDTO stock);
    }
}
