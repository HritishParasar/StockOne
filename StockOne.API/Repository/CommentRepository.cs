using Microsoft.EntityFrameworkCore;
using StockOne.API.Data;
using StockOne.API.Mapper;
using StockOne.API.Model;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddCommentAsync(CommentDTO stock)
        {
            var stockModel = stock.CreateDTO();
            await dbContext.Comments.AddAsync(stockModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int Id)
        {
            var find = await dbContext.Comments.FindAsync(Id);
            if (find != null)
            {
                dbContext.Comments.Remove(find);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = await dbContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment = await dbContext.Comments.FindAsync(id);
            return comment;
        }

        public async Task UpdateCommentAsync(int Id, CommentDTO stock)
        {
            var existingComment = await dbContext.Comments.FindAsync(Id);
            if (existingComment != null)
            {
                existingComment.Content = stock.Content;
                existingComment.Author = stock.Author;
                existingComment.CreatedAt = stock.CreatedAt;
                existingComment.StockId = stock.StockId;
                dbContext.Comments.Update(existingComment);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
