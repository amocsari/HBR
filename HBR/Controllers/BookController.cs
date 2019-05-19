using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    public class BookController : BaseController
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
            return _bookService.FindBookByIsbn(isbn, UserId);
        }

        [HttpDelete]
        public Task DeleteBookById(string bookId)
        {
            return _bookService.DeleteBookById(bookId, UserId);
        }

        [HttpPost]
        public Task AddBookToShelf([FromBody]AddBookToShelfRequest request)
        {
            return _bookService.AddBookToShelf(request, UserId);
        }

        [HttpPost]
        public Task UpdateBookProgress([FromBody]UpdateBookProgressRequest request)
        {
            return _bookService.UpdateProgress(request, UserId);
        }

        [HttpPost]
        public Task<BookDto> AddNewBook([FromBody]AddOrEditBookRequest request)
        {
            return _bookService.AddNewBook(request, UserId);
        }

        [HttpPost]
        public Task<BookDto> UpdateBook([FromBody]AddOrEditBookRequest request)
        {
            return _bookService.UpdateBook(request, UserId);
        }

        [HttpGet]
        public Task<List<BookDto>> GetMyBooks()
        {
            return _bookService.GetBooksByUser(UserId);
        }

        [HttpPost]
        public Task<List<BookDto>> GetMissingBooks([FromBody]GetMissingRequest request)
        {
            return _bookService.GetMissingBooks(request, UserId);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var stream = await _blobStorageService.GetFileFromStorageAsStream(bookId, "pdf");
            return File(stream, "application/pdf");
        }

        [HttpPost]
        public async Task UploadBook(IFormFile formFile, int bookId)
        {
            if (formFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    await _blobStorageService.UploadBook(memoryStream, bookId);
                }
            }
        }

        [HttpPost]
        public Task<List<BookDto>> BulkUpdateBooks([FromBody]List<AddOrEditBookRequest> requestList)
        {
            return _bookService.BulkUpdate(requestList, UserId);
        }
    }
}
