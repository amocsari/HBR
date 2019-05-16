using AutoMapper;
using Common.Dto;
using Common.Request;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Mappings
{
    public partial class Mappings
    {
        public static IMapperConfigurationExpression ConfigureBookmarkMapping(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Bookmark, BookmarkDto>();

            cfg.CreateMap<BookmarkDto, Bookmark>();

            return cfg;
        }
    }
}
