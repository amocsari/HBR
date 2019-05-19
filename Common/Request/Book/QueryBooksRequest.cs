using System;

namespace Common.Request
{
    public class QueryBooksRequest : RequestBase
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
