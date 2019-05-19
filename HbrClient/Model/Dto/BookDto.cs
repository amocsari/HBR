using SQLite;
using System;
using System.Collections.Generic;

namespace HbrClient.Model.Dto
{
    public class BookDto
    {
        public string BookId { get; set; }

        public string Isbn { get; set; }
        
        public string Title { get; set; }

        public string Author { get; set; }
        
        public int PageNumber { get; set; }

        public string GenreId { get; set; }

        public DateTime LastUpdated { get; set; }

        [Ignore]
        public List<BookmarkDto> Bookmarks { get; set; }
        [Ignore]
        public GenreDto Genre { get; set; }
    }
}
