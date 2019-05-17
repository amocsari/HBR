using SQLite;

namespace HbrClient.Model.Dto
{
    class ClientGenreDto : GenreDto, IClientEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ClientId { get; set; }
        public bool ModifiedOffline { get; set; }
    }
}