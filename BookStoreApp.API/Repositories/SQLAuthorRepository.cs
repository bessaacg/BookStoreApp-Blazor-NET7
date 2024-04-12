using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Domain;
using BookStoreApp.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class SQLAuthorRepository : IBookStoreAuthorRepository
    {
        private readonly BookStoreDBContext _bookStoreDBContext;

        public SQLAuthorRepository(BookStoreDBContext bookStoreDBContext)
        {
            this._bookStoreDBContext = bookStoreDBContext;
        }
        public async Task<Author> AddAuthorAsync(Author author)
        {
            await _bookStoreDBContext.AddAsync(author);
            await _bookStoreDBContext.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> DeleteAuthorAsync(int authorId)
        {
            var currentAuthor = await _bookStoreDBContext.Authors.FirstOrDefaultAsync(author => author.Id == authorId);
            if (currentAuthor == null) return null;

            _bookStoreDBContext.Remove(currentAuthor);

            await _bookStoreDBContext.SaveChangesAsync();

            return currentAuthor;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _bookStoreDBContext.Authors.ToListAsync();    
        }

        public async Task<Author?> GetAuthorByIDAsync(int id)
        {
            return await _bookStoreDBContext.Authors.FirstOrDefaultAsync(x => x.Id == id);  
        }

        public async Task<Author?> UpdateAuthorAsync(int Id, Author author)
        {
            var currentAuthor = await _bookStoreDBContext.Authors.FirstOrDefaultAsync(x => x.Id == Id);
            if (currentAuthor == null) return null;
            currentAuthor.FirstName = author.FirstName;
            currentAuthor.LastName = author.LastName;
            currentAuthor.Bio =author.Bio;

            await _bookStoreDBContext.SaveChangesAsync();   

            return currentAuthor;
        }

        
    }
}
