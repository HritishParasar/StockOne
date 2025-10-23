using StockOne.API.Model;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Mapper
{
    public static class CommentDtoMapper
    {
        public static CommentDTO MapToCommentDto(this Comment comment)
        {
            return new CommentDTO
            {
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                Author = comment.Author,
                StockId = comment.StockId
            };
        }
        public static Comment CreateDTO(this CommentDTO comment)
        {
            return new Comment
            {
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                Author = comment.Author,
                StockId = comment.StockId
            };
        }
    }
}
