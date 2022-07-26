using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookRepo : IBookRepo
    {
        private AppDbContext _context;

        public BookRepo(AppDbContext context)
        {
            _context = context;            
        }
        public async Task CreateBook(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            await _context.AddAsync(book);
        }

        public void DeleteBook(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.Books.Remove(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooks(){
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}