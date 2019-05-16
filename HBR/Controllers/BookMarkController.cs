using BLL.Services.Interface;
using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public Task<List<BookmarkDto>> GetBookmarksForBook(int bookId)
        {
            return _bookmarkService.GetBookmarksForBook(bookId);
        }

        [HttpPost]
        public Task AddBookmark([FromBody]BookmarkDto dto)
        {
            return _bookmarkService.AddBookmark(dto);
        }

        [HttpDelete]
        public Task DeleteBookMark(int bookmarkId)
        {
            return _bookmarkService.DeleteBookmark(bookmarkId);
        }
    }
}
