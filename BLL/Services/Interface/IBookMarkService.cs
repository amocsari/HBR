using Common.Dto;
using Common.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookmarkService
    {
        Task AddBookmark(BookmarkDto dto, string userIdentifier);
        Task DeleteBookmark(string bookmarkId, string userIdentifier);
        Task<List<BookmarkDto>> GetBookmarksForBook(string bookId, string userIdentifier);
        Task<List<BookmarkDto>> GetMissingBookmarks(GetMissingRequest request, string userIdentifier);
    }
}
