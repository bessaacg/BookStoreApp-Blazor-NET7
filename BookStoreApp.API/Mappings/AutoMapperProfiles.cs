using AutoMapper;
using BookStoreApp.API.Models;
using BookStoreApp.API.Models.Domain;
using BookStoreApp.API.Models.DTOs;

namespace BookStoreApp.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        { 
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<AddAuthorRequestDTO, Author>().ReverseMap();
            CreateMap<UpdateAuthorRequestDTO, Author>().ReverseMap();
            CreateMap<DeleteAuthorRequestDTO, Author>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<AddBookRequestDTO, Book>().ReverseMap();
            CreateMap<UpdateBookRequestDTO, Book>().ReverseMap();   
            CreateMap<DeleteBookRequestDTO, Book>().ReverseMap();   
        }
    }
}
