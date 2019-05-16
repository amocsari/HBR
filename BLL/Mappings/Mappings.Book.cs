using AutoMapper;
using Common.Dto;
using Common.Request;
using DAL.Entity;

namespace BLL.Mappings
{
    public static partial class Mappings
    {
        public static IMapperConfigurationExpression ConfigureBookMapping(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Book, BookDto>();

            cfg.CreateMap<AddNewBookRequest, Book>();

            cfg.CreateMap<AddBookToShelfRequest, UserBook>();

            return cfg;
        }
    }
}
