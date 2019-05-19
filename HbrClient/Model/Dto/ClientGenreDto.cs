using SQLite;

namespace HbrClient.Model.Dto
{
    class ClientGenreDto : GenreDto, IClientEntity
    {
        public bool ModifiedOffline { get; set; }
    }
}