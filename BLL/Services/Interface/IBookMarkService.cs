using Common.Dto;
using Common.Request;
using Common.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookmarkService
    {
        Task AddBookmark(AddBookmarkRequest request, string userIdentifier);
        Task DeleteBookmark(DeleteBookmarkRequest request, string userIdentifier);
        Task<List<BookmarkDto>> GetBookmarksForBook(GetBookmarksForBookRequest request, string userIdentifier);
        Task<GetMissingResponse<BookmarkDto>> GetMissingBookmarks(GetMissingRequest request, string userIdentifier);
        Task BulkInsertBookmarks(List<AddBookmarkRequest> requestList, string userIdentifier);
    }
}
