using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public Task<List<BookDto>> QueryBooks(QueryBooksRequest request)
        {
            return _bookService.QueryBooks(request);
        }

        [HttpDelete]
        public Task DeleteBookById(int bookId)
        {
            return _bookService.DeleteBookById(bookId);
        }

        [HttpPost]
        public Task AddBookToShelf([FromBody]AddBookToShelfRequest request)
        {
            return _bookService.AddBookToShelf(request);
        }

        [HttpPost]
        public Task UpdateBookProgress(UpdateBookProgressRequest request)
        {
            return _bookService.UpdateProgress(request);
        }

        [HttpPost]
        public Task<BookDto> AddNewBook([FromBody]AddNewBookRequest request)
        {
            return _bookService.AddNewBook(request);
        }

        [HttpPost]
        public Task<BookDto> UpdateBook([FromBody]UpdateBookRequest request)
        {
            return _bookService.UpdateBook(request);
        }

        [HttpGet]
        public Task<List<BookDto>> GetMyBooks()
        {
            return _bookService.GetMyBooks();
        }
    }
}
