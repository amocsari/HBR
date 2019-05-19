using System;

namespace HbrClient.Model.Dto
{
    public class BookmarkDto
    {
        public string BookmarkId { get; set; }
        
        public string BookId { get; set; }
        
        public int PageNumber { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
