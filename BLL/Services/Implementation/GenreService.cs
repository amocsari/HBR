using AutoMapper;
using BLL.Services.Interface;
using Common.Dto;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IHbrDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(IHbrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            var queryResult = await _context.Genres.AsNoTracking().ToListAsync();
            var genreList = _mapper.Map<List<GenreDto>>(queryResult);

            return genreList;
        }
    }
}
