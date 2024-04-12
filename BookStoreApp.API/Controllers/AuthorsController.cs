using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Domain;
using BookStoreApp.API.Models.DTOs;
using BookStoreApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDBContext _authorDbContext;
        private readonly IBookStoreAuthorRepository _bookStoreAuthorRepository;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public AuthorsController(BookStoreDBContext authorDbContext, 
            IBookStoreAuthorRepository bookStoreAuthorRepository,
            ILogger<AuthorsController> logger,
            IMapper mapper)
        {
            this._authorDbContext = authorDbContext;
            this._bookStoreAuthorRepository = bookStoreAuthorRepository;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetAuthors")]
        public async Task<ActionResult> GetAuthors()
        {
            try
            {
                IEnumerable<Author> authorsDomain = await _bookStoreAuthorRepository.GetAllAuthorsAsync();

                _logger.LogInformation($"Finished GetAuthors request with data {JsonSerializer.Serialize(authorsDomain)}");

                var authorsDTO = _mapper.Map<List<Author>>(authorsDomain);

                return Ok(authorsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                throw;
            }
        }

        [HttpGet]
        [Route("GetAuthorById/{Id}")]
        public async Task<ActionResult> GetAuthorById(int Id)
        {
            Author? authorDomain = await _bookStoreAuthorRepository.GetAuthorByIDAsync(Id);
            
            if(authorDomain == null) return NotFound($"Author with Id {Id} could not be found");

            var authorDTO = _mapper.Map<AuthorDTO>(authorDomain);

            return Ok(authorDTO);   
        }

        [HttpPost]
        [Route("AddAuthor")]
        public async Task<ActionResult> AddAuthor([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomainModel = _mapper.Map<Author>(addAuthorRequestDTO);
               
            authorDomainModel = await _bookStoreAuthorRepository.AddAuthorAsync(authorDomainModel);

            var authorDTO = _mapper.Map<AuthorDTO>(authorDomainModel);
                

            return CreatedAtAction(nameof(GetAuthorById), new { Id = authorDTO.Id }, authorDTO);
        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult?> UpdateAuthor([FromRoute]int Id, [FromBody] UpdateAuthorRequestDTO updateAuthorRequestDTO)
        {
            var authorDomainModel = _mapper.Map<Author>(updateAuthorRequestDTO);

            authorDomainModel = await _bookStoreAuthorRepository.UpdateAuthorAsync(Id, authorDomainModel);

            if (authorDomainModel == null) return NotFound($"Author with id {Id} does not exist");

            var authorDTO = _mapper.Map<AuthorDTO>(authorDomainModel);

            return Ok(authorDTO);
        }

    }
}
