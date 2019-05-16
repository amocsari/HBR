using BLL.Services.Interface;
using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            this._genreService = genreService;
        }

        [HttpGet]
        public Task<List<GenreDto>> GetAllGenres()
        {
            return _genreService.GetAllGenres();
        }
    }
}
