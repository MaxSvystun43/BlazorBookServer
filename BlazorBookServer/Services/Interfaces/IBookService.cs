using BlazorBookApp.Data;

namespace BlazorBookApp.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int Id);
        Task<Book> UpdateBookAsync(Book book);
        Task<List<Book>> SearchByString(string data);

    }
}
