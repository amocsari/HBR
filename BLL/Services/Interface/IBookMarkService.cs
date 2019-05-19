using Common.Dto;
using Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookmarkService
    {
        Task AddBookmark(BookmarkDto dto, string userIdentifier);
        Task DeleteBookmark(int bookmarkId, string userIdentifier);
        Task<List<BookmarkDto>> GetBookmarksForBook(int bookId, string userIdentifier);
        Task<List<BookmarkDto>> GetMissingBookmarks(GetMissingRequest request, string userIdentifier);
    }
}
