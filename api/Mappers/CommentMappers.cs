using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    // This class is used to map the Comment model to the CommentDto and vice versa
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
                CreateBy = comment.AppUser.UserName,
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