using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Request
{
    public class AddNewBookRequest
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PageNumber { get; set; }

        public int? GenreId { get; set; }

        public bool AutoCompleteData { get; set; }
    }
}
