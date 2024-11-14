using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfraces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        

        public async Task<List<Comment>> GetComments()
        {
            return  await _context.Comments.ToListAsync();         
        }
        public  async Task<Comment?> GetCommentID(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateComment(int id, Comment comment)
        {
            var existingComment =  await _context.Comments.FindAsync(id);
            if(existingComment == null)
            {
                return null;
            }
            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}