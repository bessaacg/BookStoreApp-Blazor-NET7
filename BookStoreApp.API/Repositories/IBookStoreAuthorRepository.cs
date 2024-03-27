using BookStoreApp.API.Models;

namespace BookStoreApp.API.Repositories
{
    public interface IBookStoreAuthorRepository
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIDAsync(int id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author?> UpdateAuthorAsync(int Id, Author author);
        Task<Author?> DeleteAuthorAsync(int authorId);
    }
}
