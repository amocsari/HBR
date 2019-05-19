using AutoMapper;
using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using DAL;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IHbrDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;

        public BookmarkService(IHbrDbContext context, IMapper mapper, ITimeService timeService)
        {
            _context = context;
            _mapper = mapper;
            _timeService = timeService;
        }

        public async Task AddBookmark(BookmarkDto dto, string userIdentifier)
        {
            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == dto.BookId)
                ?? throw new HbrException("Ilyen azonosítójú könyv nem létezik!");

            if (book.PageNumber < dto.PageNumber)
                throw new HbrException("A könyvjelző oldalszáma nem lehet nagyobb mint a könyv oldalainak száma!");

            var entity = _mapper.Map<Bookmark>(dto);
            entity.LastUpdated = _timeService.UtcNow;

            await _context.Bookmarks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookmark(string bookmarkId, string userIdentifier)
        {
            //ignore the queryfilters to aviod unnecessarry errors upon multiple deletion request on the same book
            var entity = await _context.Bookmarks.IgnoreQueryFilters().FirstOrDefaultAsync(bm => bm.BookmarkId == bookmarkId)
                ?? throw new HbrException("Nem létező könyvjelző!");

            entity.Deleted = true;
            entity.LastUpdated = _timeService.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookmarkDto>> GetBookmarksForBook(string bookId, string userIdentifier)
        {
            await CheckIfBookExists(bookId);

            var queryResult = await _context.Bookmarks.AsNoTracking().Where(bm => bm.BookId == bookId).ToListAsync();

            var bookmarkList = _mapper.Map<List<BookmarkDto>>(queryResult);
            return bookmarkList;
        }

        public async Task<List<BookmarkDto>> GetMissingBookmarks(GetMissingRequest request, string userIdentifier)
        {
            var missingBookmarks = await _context.Bookmarks
                .Where(bm => bm.UserIdentifier == userIdentifier)
                .Where(bm => !request.IdList.Contains(bm.BookmarkId)).ToListAsync();

            return _mapper.Map<List<BookmarkDto>>(missingBookmarks);
        }

        private async Task CheckIfBookExists(string bookId)
        {
            var bookExists = await _context.Books.AsNoTracking().AnyAsync(b => b.BookId == bookId);
            if (!bookExists)
                throw new HbrException("Ilyen azonosítójú könyv nem létezik!");
        }
    }
}
