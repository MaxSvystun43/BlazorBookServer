using BlazorBookApp.Data;
using BlazorBookApp.Services.Interfaces;
using BlazorBookServer.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookApp.Services
{
    public class BookService: IBookService
    {
        private ApplicationDbContext _context;

        public BookService(ApplicationDbContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> GetBookByIdAsync(int Id)
        {
            var books = await _context.Books.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return books;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> SearchByString(string data) 
        {
            var result = await _context.Books.Where(x => x.Title.Contains(data)).ToListAsync();
            result.AddRange(await _context.Books.Where(x => x.Author.Contains(data)).ToListAsync());
            return result.DistinctBy(x => x.Id).ToList();
        }
    }
}
