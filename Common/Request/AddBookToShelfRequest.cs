using System;

namespace Common.Request
{
    public class AddBookToShelfRequest
    {
        public string BookId { get; set; }
        public int? Progress { get; set; }
    }
}
