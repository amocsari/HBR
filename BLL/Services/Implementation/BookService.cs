using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services.Interface;
using Common.Dto;
using Common.Request;
using DAL;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IHbrDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGoodReadsApiService _goodReadsService;
        private readonly ITimeService _timeService;

        public BookService(IHbrDbContext context, IMapper mapper, IGoodReadsApiService goodReadsService, ITimeService timeService)
        {
            _context = context;
            _mapper = mapper;
            _goodReadsService = goodReadsService;
            _timeService = timeService;
        }

        public async Task AddBookToShelf(AddBookToShelfRequest request)
        {
            var entity = _mapper.Map<UserBook>(request);

            var alreadyAdded = await _context.UserBooks.AsNoTracking().AnyAsync(ub => ub.BookId == request.BookId && ub.UserId == request.UserId);

            if (alreadyAdded)
                throw new HbrException("Ez a könyv már hozzá van adva ennek a felhasználónak a polcához!");

            await _context.UserBooks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BookDto> AddNewBook(AddNewBookRequest request)
        {
            var entity = _mapper.Map<Book>(request);

            if (request.AutoCompleteData && !string.IsNullOrEmpty(request.Isbn))
            {
                try
                {
                    await _goodReadsService.TryGetGoodReadsData(request.Isbn, entity);
                }
                catch (Exception e)
                {
                    entity.Title = request.Title;
                    entity.Author = request.Author;
                }
            }
            else
            {
                entity.Title = request.Title;
                entity.Author = request.Author;
            }
            entity.PageNumber = request.PageNumber;
            entity.Isbn = request.Isbn;
            entity.GenreId = request.GenreId;
            entity.LastUpdated = _timeService.UtcNow;

            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();

            var newEntity = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId == entity.BookId);

            return _mapper.Map<BookDto>(newEntity);
        }

        public async Task DeleteBookById(int bookId)
        {
            //ignore the queryfilters to aviod unnecessarry errors upon multiple deletion request on the same book
            var entity = await _context.Books.IgnoreQueryFilters().FirstOrDefaultAsync(book => book.BookId == bookId)
                ?? throw new HbrException("Ismeretlen BookId");

            entity.Deleted = true;
            entity.LastUpdated = _timeService.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<BookHeaderDto> FindBookByIsbn(string isbn)
        {
            return await _goodReadsService.FindBookByIsbn(isbn);
        }

        public async Task<List<BookDto>> GetMissingBooks(GetMissingRequest request)
        {
            //TODO user query
            var missingBooks = await _context.Books
                .AsNoTracking()
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .Where(b => !request.IdList.Contains(b.BookId))
                .ToListAsync();

            return _mapper.Map<List<BookDto>>(missingBooks);
        }

        public async Task<List<BookDto>> GetMyBooks()
        {
            //TODO user query (usergroups)
            var bookList = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<BookDto>>(bookList);
        }

        public async Task<List<BookDto>> QueryBooks(QueryBooksRequest request)
        {
            var bookQuery = _context.Books
                .Include(b => b.Genre)
                .AsNoTracking();

            if (request.Genre.HasValue)
            {
                bookQuery = bookQuery.Where(book => book.GenreId == request.Genre.Value);
            }

            if (!string.IsNullOrEmpty(request.Author))
            {
                bookQuery = bookQuery.Where(book => book.Author.Contains(request.Author));
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                bookQuery = bookQuery.Where(book => book.Title.Contains(request.Title));
            }

            var bookList = await bookQuery.ToListAsync();
            return _mapper.Map<List<BookDto>>(bookList);
        }

        public async Task<BookDto> UpdateBook(UpdateBookRequest request)
        {

            var entity = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .FirstOrDefaultAsync(b => b.BookId == request.BookId)
                ?? throw new HbrException("Ismeretlen könyvazonosító!");

            if (request.AutoCompleteData && !string.IsNullOrEmpty(request.Isbn))
            {
                try
                {
                    await _goodReadsService.TryGetGoodReadsData(request.Isbn, entity);
                }catch(Exception e)
                {
                    entity.Title = request.Title;
                    entity.Author = request.Author;
                }
            }
            else
            {
                entity.Title = request.Title;
                entity.Author = request.Author;
            }
            entity.PageNumber = request.PageNumber;
            entity.Isbn = request.Isbn;
            entity.GenreId = request.GenreId;
            entity.LastUpdated = _timeService.UtcNow;

            await _context.SaveChangesAsync();

            var newDto = _mapper.Map<BookDto>(entity);

            return newDto;
        }

        public async Task UpdateProgress(UpdateBookProgressRequest request)
        {
            throw new NotImplementedException();
            //TODO set user query
            var userBook = await _context.UserBooks.FirstOrDefaultAsync(ub => ub.BookId == request.BookId);
            userBook.Progress = request.NewProgress;
            await _context.SaveChangesAsync();
        }
    }
}
