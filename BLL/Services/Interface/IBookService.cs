using Common.Dto;
using Common.Request;
using Common.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookService
    {
        Task<BookDto> AddNewBook(AddOrEditBookRequest request, string userIdentifier);

        Task<List<BookDto>> QueryBooks(QueryBooksRequest request, string userIdentifier);

        Task DeleteBookById(DeleteBookByIdRequest request, string userIdentifier);

        Task AddBookToShelf(AddBookToShelfRequest request, string userIdentifier);

        Task UpdateProgress(UpdateBookProgressRequest request, string userIdentifier);

        Task<BookDto> UpdateBook(AddOrEditBookRequest request, string userIdentifier);

        Task<List<BookDto>> GetBooksByUser(string userIdentifier);
        Task<List<BookDto>> GetBooksByUploader(string userIdentifier);

        Task<BookHeaderDto> FindBookByIsbn(string isbn);
        Task<GetMissingResponse<BookDto>> GetMissingBooks(GetMissingRequest request, string userIdentifier);

        Task<List<BookDto>> BulkUpdate(List<AddOrEditBookRequest> requestList, string userIdentifier);
        Task RemoveFromShelf(RemoveFromShelfRequest request, string userIdentifier);
    }
}
