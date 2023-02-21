using BlazorBookApp.Data;
using BlazorBookApp.Services.Interfaces;
using BlazorBookServer.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookApp.Services
{
    public class CommentService : ICommentService
    {
        private ApplicationDbContext _context;

        public CommentService(ApplicationDbContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<List<Comment>> GetAllCommentsByBookIdAsync(int bookId)
        {
            return await _context.Comments.Where(x => x.BookId == bookId).ToListAsync();
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.Where(x => x.Id.Equals(commentId)).FirstOrDefaultAsync();

            if (comment == null)
            {
                throw new Exception($"There is no commend with {commentId}");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
