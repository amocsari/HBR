using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    public class BookmarkController : BaseController
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public Task<List<BookmarkDto>> GetBookmarksForBook(string bookId)
        {
            return _bookmarkService.GetBookmarksForBook(bookId, UserId);
        }

        [HttpPost]
        public Task AddBookmark([FromBody]BookmarkDto dto)
        {
            return _bookmarkService.AddBookmark(dto, UserId);
        }

        [HttpDelete]
        public Task DeleteBookMark(string bookmarkId)
        {
            return _bookmarkService.DeleteBookmark(bookmarkId, UserId);
        }

        [HttpGet]
        public Task GetMissingBookmarks(GetMissingRequest request)
        {
            return _bookmarkService.GetMissingBookmarks(request, UserId);
        }
    }
}
