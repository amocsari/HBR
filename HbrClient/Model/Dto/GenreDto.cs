using SQLite;

namespace HbrClient.Model.Dto
{
    public class GenreDto
    {
        [PrimaryKey]
        public string GenreId { get; set; }
        
        public string GenreName { get; set; }
    }
}
