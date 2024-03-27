using BookStoreApp.API.Models;

namespace BookStoreApp.API.Repositories
{
    public interface IBookStoreBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIDAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book?> UpdateBookAsync(int Id, Book book);
        Task<Book?> DeleteRegionAsync(int bookId);
    }
}
