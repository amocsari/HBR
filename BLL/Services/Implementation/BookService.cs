﻿using System;
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

        public async Task AddBookToShelf(AddBookToShelfRequest request, string userIdentifier)
        {
            var entity = _mapper.Map<UserBook>(request);
            entity.UserIdentifier = userIdentifier;

            var alreadyAdded = await _context.UserBooks.AsNoTracking().AnyAsync(ub => ub.BookId == request.BookId && ub.UserIdentifier == userIdentifier);

            if (alreadyAdded)
                throw new HbrException("Ez a könyv már hozzá van adva ennek a polcához!");

            await _context.UserBooks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BookDto> AddNewBook(AddOrEditBookRequest request, string userIdentifier)
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
            entity.Extension = "pdf";
            entity.UploaderIdentifier = userIdentifier;

            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();

            var newEntity = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId == entity.BookId);

            return _mapper.Map<BookDto>(newEntity);
        }

        public async Task<List<BookDto>> BulkUpdate(List<AddOrEditBookRequest> requestList, string userIdentifier)
        {
            var entitiesToUpdate = await _context.Books
                .Where(b => requestList.Any(r => r.BookId == b.BookId))
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .ToListAsync();

            foreach (var entity in entitiesToUpdate)
            {
                var request = requestList.FirstOrDefault(r => r.BookId == entity.BookId);

                entity.Title = request.Title;
                entity.Author = request.Author;
                entity.PageNumber = request.PageNumber;
                entity.Isbn = request.Isbn;
                entity.GenreId = request.GenreId;
                entity.LastUpdated = _timeService.UtcNow;
            }

            var requestsToInsert = requestList
                .Where(b => !entitiesToUpdate.Any(e => e.BookId == b.BookId))
                .ToList();
            var entitiesToInsert = _mapper.Map<List<Book>>(requestsToInsert);

            foreach (var entity in entitiesToInsert)
            {
                entity.LastUpdated = _timeService.UtcNow;
                entity.Extension = "pdf";
            }
            await _context.Books.AddRangeAsync(entitiesToInsert);
            await _context.SaveChangesAsync();

            var insertedEntityList = _context.Books
                .AsNoTracking()
                .Include(b => b.Bookmarks)
                .Include(b => b.Genre)
                .Where(b => entitiesToInsert.Any(e => e.BookId == b.BookId));

            var insertedDtoList = _mapper.Map<List<BookDto>>(insertedEntityList);
            var updatedDtoList = _mapper.Map<List<BookDto>>(entitiesToUpdate);

            updatedDtoList.AddRange(insertedDtoList);
            return updatedDtoList;
        }

        public async Task DeleteBookById(string bookId, string userIdentifier)
        {
            //ignore the queryfilters to aviod unnecessarry errors upon multiple deletion request on the same book
            var entity = await _context.Books.IgnoreQueryFilters().FirstOrDefaultAsync(book => book.BookId == bookId)
                ?? throw new HbrException("Ismeretlen BookId");

            entity.Deleted = true;
            entity.LastUpdated = _timeService.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<BookHeaderDto> FindBookByIsbn(string isbn, string userIdentifier)
        {
            return await _goodReadsService.FindBookByIsbn(isbn);
        }

        public async Task<List<BookDto>> GetMissingBooks(GetMissingRequest request, string userIdentifier)
        {
            //TODO user query
            var missingBooks = await _context.UserBooks
                .AsNoTracking()
                .Where(ub => ub.UserIdentifier == userIdentifier)
                .Select(ub => ub.Book)
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .Where(b => !request.IdList.Contains(b.BookId))
                .ToListAsync();

            return _mapper.Map<List<BookDto>>(missingBooks);
        }

        public async Task<List<BookDto>> GetBooksByUser(string userIdentifier)
        {
            var bookList = await _context.UserBooks
                .Where(ub => ub.UserIdentifier == userIdentifier)
                .Select(ub => ub.Book)
                .Include(b => b.Bookmarks)
                .Include(b => b.Genre)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<BookDto>>(bookList);
        }

        public async Task<List<BookDto>> QueryBooks(QueryBooksRequest request)
        {
            var bookQuery = _context.Books
                .Include(b => b.Genre)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.Genre))
            {
                bookQuery = bookQuery.Where(book => book.GenreId == request.Genre);
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

        public async Task<BookDto> UpdateBook(AddOrEditBookRequest request, string userIdentifier)
        {

            var entity = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Bookmarks)
                .FirstOrDefaultAsync(b => b.BookId == request.BookId)
                ?? throw new HbrException("Ismeretlen könyvazonosító!");

            if (entity.UploaderIdentifier != userIdentifier)
                throw new HbrException("Nem saját feltöltött könyvek nem szerkesztetőek!");

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

            await _context.SaveChangesAsync();

            var newDto = _mapper.Map<BookDto>(entity);

            return newDto;
        }

        public async Task UpdateProgress(UpdateBookProgressRequest request, string userIdentifier)
        {
            var userBook = await _context.UserBooks.FirstOrDefaultAsync(ub => ub.BookId == request.BookId && ub.UserIdentifier == userIdentifier);
            userBook.Progress = request.NewProgress;
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookDto>> GetBooksByUploader(string userIdentifier)
        {
            var entities = await _context.Books.AsNoTracking().Where(b => b.UploaderIdentifier == userIdentifier).ToListAsync();

            return _mapper.Map<List<BookDto>>(entities);
        }
    }
}
