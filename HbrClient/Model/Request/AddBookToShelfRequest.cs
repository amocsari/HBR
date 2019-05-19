using System;

namespace HbrClient.Model.Request
{
    public class AddBookToShelfRequest
    {
        public string BookId { get; set; }
        public int? Progress { get; set; }
    }
}
