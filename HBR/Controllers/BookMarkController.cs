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
        public Task<List<BookmarkDto>> GetBookmarksForBook(GetBookmarksForBookRequest request)
        {
            //return _bookmarkService.GetBookmarksForBook(request, UserId);
            #region tmp ki lesz veve
            return _bookmarkService.GetBookmarksForBook(request, request.UserIdentifier);
            #endregion
        }

        [HttpPost]
        public Task AddBookmark([FromBody]AddBookmarkRequest request)
        {
            //return _bookmarkService.AddBookmark(request, UserId);
            #region tmp ki lesz veve
            return _bookmarkService.AddBookmark(request, request.UserIdentifier);
            #endregion
        }

        [HttpDelete]
        public Task DeleteBookMark(DeleteBookmarkRequest request)
        {
            //return _bookmarkService.DeleteBookmark(request, UserId);
            #region tmp ki lesz veve
            return _bookmarkService.DeleteBookmark(request, request.UserIdentifier);
            #endregion
        }

        [HttpGet]
        public Task GetMissingBookmarks(GetMissingRequest request)
        {
            //return _bookmarkService.GetMissingBookmarks(request, UserId);
            #region tmp ki lesz veve
            return _bookmarkService.GetMissingBookmarks(request, request.UserIdentifier);
            #endregion
        }
    }
}
