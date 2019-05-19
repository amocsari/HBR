using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            return _bookService.FindBookByIsbn(isbn);
        }

        [HttpPost]
        public Task DeleteBookById([FromBody]DeleteBookByIdRequest request)
        {
            //return _bookService.DeleteBookById(request, UserId);
            #region tmp ki lesz veve
            return _bookService.DeleteBookById(request, request.UserIdentifier);
            #endregion
        }

        [HttpPost]
        public Task AddBookToShelf([FromBody]AddBookToShelfRequest request)
        {
            //return _bookService.AddBookToShelf(request, UserId);
            #region tmp ki lesz veve
            return _bookService.AddBookToShelf(request, request.UserIdentifier);
            #endregion
        }

        [HttpPost]
        public Task UpdateBookProgress([FromBody]UpdateBookProgressRequest request)
        {
            //return _bookService.UpdateProgress(request, UserId);
            #region tmp ki lesz veve
            return _bookService.UpdateProgress(request, request.UserIdentifier);
            #endregion
        }

        [HttpPost]
        public Task<BookDto> AddNewBook([FromBody]AddOrEditBookRequest request)
        {
            //return _bookService.AddNewBook(request, UserId);
            #region tmp ki lesz veve
            return _bookService.AddNewBook(request, request.UserIdentifier);
            #endregion
        }

        [HttpPost]
        public Task<BookDto> UpdateBook([FromBody]AddOrEditBookRequest request)
        {
            //return _bookService.UpdateBook(request, UserId);
            #region tmp ki lesz veve
            return _bookService.UpdateBook(request, request.UserIdentifier);
            #endregion
        }

        //[HttpGet]
        //public Task<List<BookDto>> GetMyBooks()
        //{
        //    return _bookService.GetBooksByUser(UserId);
        //}

        #region tmp ki lesz veve
        [HttpGet]
        public Task<List<BookDto>> GetMyBooks(string userIdentifier)
        {
            return _bookService.GetBooksByUser(userIdentifier);
        }
        #endregion

        [HttpPost]
        public Task<GetMissingResponse<BookDto>> GetMissingBooks([FromBody]GetMissingRequest request)
        {
            //return _bookService.GetMissingBooks(request, UserId);
            #region tmp ki lesz veve
            return _bookService.GetMissingBooks(request, request.UserIdentifier);
            #endregion
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
            //return _bookService.BulkUpdate(requestList, UserId);
            #region tmp ki lesz veve
            return _bookService.BulkUpdate(requestList, requestList.First().UserIdentifier);
            #endregion
        }
    }
}
