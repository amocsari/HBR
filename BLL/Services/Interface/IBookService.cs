﻿using Common.Dto;
using Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookService
    {
        Task<BookDto> AddNewBook(AddNewBookRequest request, string userIdentifier);

        Task<List<BookDto>> QueryBooks(QueryBooksRequest request);

        Task DeleteBookById(int bookId, string userIdentifier);

        Task AddBookToShelf(AddBookToShelfRequest request, string userIdentifier);

        Task UpdateProgress(UpdateBookProgressRequest request, string userIdentifier);

        Task<BookDto> UpdateBook(UpdateBookRequest request, string userIdentifier);

        Task<List<BookDto>> GetBooksByUser(string userIdentifier);
        Task<List<BookDto>> GetBooksByUploader(string userIdentifier);

        Task<BookHeaderDto> FindBookByIsbn(string isbn, string userIdentifier);
        Task<List<BookDto>> GetMissingBooks(GetMissingRequest request, string userIdentifier);

        Task<List<BookDto>> BulkInsert(List<AddNewBookRequest> requestList, string userIdentifier);
        Task<List<BookDto>> BulkUpdate(List<UpdateBookRequest> requestList, string userIdentifier);
    }
}
