using Common.Dto;
using Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookmarkService
    {
        Task AddBookmark(BookmarkDto dto);
        Task DeleteBookmark(int bookmarkId);
        Task<List<BookmarkDto>> GetBookmarksForBook(int bookId);
    }
}
