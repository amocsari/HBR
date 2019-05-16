using AutoMapper;
using Common.Dto;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Mappings
{
    public static partial class Mappings
    {
        public static IMapperConfigurationExpression ConfigureGenreMapping(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Genre, GenreDto>();

            return cfg;
        }
    }
}
