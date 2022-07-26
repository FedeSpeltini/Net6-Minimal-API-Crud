using BookShop.Models;

namespace BookShop.Data
{
    public interface IBookRepo
    {
        Task SaveChanges();
        Task<Book?> GetBookById(int id);
        Task<IEnumerable<Book>> GetAllBooks();
        Task CreateBook(Book book);
        void DeleteBook(Book book);
    }
}