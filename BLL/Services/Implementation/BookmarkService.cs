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

        public async Task AddBookmark(AddBookmarkRequest request, string userIdentifier)
        {
            var book = await _context.Book.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == request.BookId)
                ?? throw new HbrException("Ilyen azonosítójú könyv nem létezik!");

            if (book.PageNumber < request.PageNumber)
                throw new HbrException("A könyvjelző oldalszáma nem lehet nagyobb mint a könyv oldalainak száma!");

            var entity = _mapper.Map<Bookmark>(request);
            entity.LastUpdated = _timeService.UtcNow;

            await _context.Bookmark.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookmark(DeleteBookmarkRequest request, string userIdentifier)
        {
            //ignore the queryfilters to aviod unnecessarry errors upon multiple deletion request on the same book
            var entity = await _context.Bookmark.IgnoreQueryFilters().FirstOrDefaultAsync(bm => bm.BookmarkId == request.BookmarkId)
                ?? throw new HbrException("Nem létező könyvjelző!");

            if (entity.UserIdentifier != userIdentifier)
                throw new HbrException("Csak csaját könyvjelzőt lehet törölni!");

            entity.Deleted = true;
            entity.LastUpdated = _timeService.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookmarkDto>> GetBookmarksForBook(GetBookmarksForBookRequest request, string userIdentifier)
        {
            await CheckIfBookExists(request.BookId);

            var queryResult = await _context.Bookmark.AsNoTracking().Where(bm => bm.BookId == request.BookId).ToListAsync();

            var bookmarkList = _mapper.Map<List<BookmarkDto>>(queryResult);
            return bookmarkList;
        }

        public async Task<List<BookmarkDto>> GetMissingBookmarks(GetMissingRequest request, string userIdentifier)
        {
            var missingBookmarks = await _context.Bookmark
                .Where(bm => bm.UserIdentifier == userIdentifier)
                .Where(bm => !request.IdList.Contains(bm.BookmarkId)).ToListAsync();

            return _mapper.Map<List<BookmarkDto>>(missingBookmarks);
        }

        private async Task CheckIfBookExists(string bookId)
        {
            var bookExists = await _context.Book.AsNoTracking().AnyAsync(b => b.BookId == bookId);
            if (!bookExists)
                throw new HbrException("Ilyen azonosítójú könyv nem létezik!");
        }
    }
}
