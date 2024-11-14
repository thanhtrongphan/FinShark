using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto toCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockID = comment.StockID
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto comment, int stockID)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockID = stockID
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentRequest comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
            };
        }
    }
}