using BlazorBookApp.Data;

namespace BlazorBookApp.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllCommentsByBookIdAsync(int bookId);
        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int commentId);
    }
}
