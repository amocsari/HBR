using Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllGenres();
    }
}
