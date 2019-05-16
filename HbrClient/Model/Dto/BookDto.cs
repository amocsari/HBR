using Newtonsoft.Json;
using System.Collections.Generic;

namespace HbrClient.Model.Dto
{
    public class BookDto
    {
        public int? BookId { get; set; }

        public string Isbn { get; set; }
        
        public string Title { get; set; }

        public string Author { get; set; }
        
        public int PageNumber { get; set; }

        public int? GenreId { get; set; }

        public List<BookmarkDto> Bookmarks { get; set; }
        public GenreDto Genre { get; set; }
    }
}
