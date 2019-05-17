﻿using System;

namespace Common.Dto
{
    public class BookmarkDto
    {
        public int BookmarkId { get; set; }
        
        public int BookId { get; set; }
        
        public int PageNumber { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
