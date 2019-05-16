using Common.Dto;
using Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBookService
    {
        Task<BookDto> AddNewBook(AddNewBookRequest request);

        Task<List<BookDto>> QueryBooks(QueryBooksRequest request);

        Task DeleteBookById(int bookId);

        Task AddBookToShelf(AddBookToShelfRequest request);

        Task UpdateProgress(UpdateBookProgressRequest request);

        Task<BookDto> UpdateBook(UpdateBookRequest request);

        Task<List<BookDto>> GetMyBooks();
    }
}
