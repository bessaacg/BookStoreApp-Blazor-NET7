using BookStoreApp.API.Data;
using BookStoreApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class SQLBookRepository : IBookStoreBookRepository
    {
        private readonly BookStoreDBContext _bookStoreDBContext;

        public SQLBookRepository(BookStoreDBContext bookStoreDBContext)
        {
            this._bookStoreDBContext = bookStoreDBContext;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookStoreDBContext.AddRangeAsync(book);
            await _bookStoreDBContext.SaveChangesAsync();   
            return book;
        }

        public async Task<Book?> DeleteRegionAsync(int bookId)
        {
            var currentBook = await _bookStoreDBContext.Books.FirstOrDefaultAsync(x=>x.Id == bookId);
            if (currentBook == null) return null;

            _bookStoreDBContext.Books.Remove(currentBook);
            await _bookStoreDBContext.SaveChangesAsync();
            return currentBook;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookStoreDBContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIDAsync(int id)
        {
            return await _bookStoreDBContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book?> UpdateBookAsync(int Id, Book book)
        {
            var currentBook = await _bookStoreDBContext.Books.FirstOrDefaultAsync(x => x.Id == Id);
            if (currentBook == null) return null;

            currentBook.Id = book.Id;
            currentBook.Title = book.Title;
            currentBook.Price = book.Price;
            currentBook.Year = book.Year;
            currentBook.ISBN = book.ISBN;
            currentBook.Author = book.Author;
            currentBook.AuthorId = book.AuthorId;
            currentBook.Summary = book.Summary;
            currentBook.Year = book.Year;
            currentBook.Image = book.Image;
            currentBook.Price = book.Price;
            
            await _bookStoreDBContext.AddRangeAsync(currentBook);
            return currentBook;
        }
    }
}
