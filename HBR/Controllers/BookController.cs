using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBlobStorageService _blobStorageService;

        public BookController(IBookService bookService, IBlobStorageService blobStorageService)
        {
            _bookService = bookService;
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        public Task<List<BookDto>> QueryBooks(QueryBooksRequest request)
        {
            return _bookService.QueryBooks(request);
        }

        [HttpGet]
        public Task<BookHeaderDto> FindBookByIsbn(string isbn)
        {
            return _bookService.FindBookByIsbn(isbn);
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

        [HttpGet]
        public Task<List<BookDto>> GetMissingBooks(GetMissingRequest request)
        {
            return _bookService.GetMissingBooks(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var stream = await _blobStorageService.GetFileFromStorageAsStream(bookId, "pdf");
            //return stream.GetBuffer();
            return File(stream, "application/pdf");
        }
    }
}
