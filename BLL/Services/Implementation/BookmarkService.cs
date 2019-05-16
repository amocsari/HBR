using AutoMapper;
using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using DAL;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IHbrDbContext _context;
        private readonly IMapper _mapper;

        public BookmarkService(IHbrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddBookmark(BookmarkDto dto)
        {
            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == dto.BookId)
                ?? throw new HbrException("Ilyen azonosítójú könyv nem létezik!");

            if (book.PageNumber < dto.PageNumber)
                throw new HbrException("A könyvjelző oldalszáma nem lehet nagyobb mint a könyv oldalainak száma!");

            var entity = _mapper.Map<Bookmark>(dto);

            await _context.Bookmarks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookmark(int bookmarkId)
        {
            //ignore the queryfilters to aviod unnecessarry errors upon multiple deletion request on the same book
            var entity = await _context.Bookmarks.IgnoreQueryFilters().FirstOrDefaultAsync(bm => bm.BookmarkId == bookmarkId)
                ?? throw new HbrException("Nem létező könyvjelző!");

            entity.Deleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookmarkDto>> GetBookmarksForBook(int bookId)
        {
            await CheckIfBookExists(bookId);
    
            var queryResult = await _context.Bookmarks.AsNoTracking().Where(bm => bm.BookId == bookId).ToListAsync();

            var bookmarkList = _mapper.Map<List<BookmarkDto>>(queryResult);
            return bookmarkList;
        }

        private async Task CheckIfBookExists(int bookId)
        {
            var bookExists = await _context.Books.AsNoTracking().AnyAsync(b => b.BookId == bookId);
            if (!bookExists)
                throw new HbrException("Ilyen azonosítójú könyv nem létezik!");
        }
    }
}
