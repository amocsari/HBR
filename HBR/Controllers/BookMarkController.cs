using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using Common.Response;
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

        [HttpPost]
        public Task<List<BookmarkDto>> GetBookmarksForBook([FromBody]GetBookmarksForBookRequest request)
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

        [HttpPost]
        public Task DeleteBookMark([FromBody]DeleteBookmarkRequest request)
        {
            //return _bookmarkService.DeleteBookmark(request, UserId);
            #region tmp ki lesz veve
            return _bookmarkService.DeleteBookmark(request, request.UserIdentifier);
            #endregion
        }

        [HttpPost]
        public Task<GetMissingResponse<BookmarkDto>> GetMissingBookmarks([FromBody]GetMissingRequest request)
        {
            //return _bookmarkService.GetMissingBookmarks(request, UserId);
            #region tmp ki lesz veve
            return _bookmarkService.GetMissingBookmarks(request, request.UserIdentifier);
            #endregion
        }
    }
}
