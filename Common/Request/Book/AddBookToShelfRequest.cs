using System;

namespace Common.Request
{
    public class AddBookToShelfRequest : RequestBase
    {
        public string BookId { get; set; }
        public int? Progress { get; set; }
    }
}
