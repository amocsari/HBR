using AutoMapper;

namespace BLL.Mappings
{
    public static partial class Mappings
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg
                    .ConfigureBookMapping()
                    .ConfigureBookmarkMapping()
                    .ConfigureGenreMapping();
            });

            return config.CreateMapper();
        }
    }
}
