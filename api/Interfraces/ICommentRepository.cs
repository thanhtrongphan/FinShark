using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfraces
{
    // Interface for Comment Repository
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetComments();
        public Task<Comment?> GetCommentID(int id);
        public Task<Comment> CreateComment(Comment comment);
        public Task<Comment?> UpdateComment(int id, Comment comment);
        public Task<Comment?> DeleteComment(int id);
    }
}