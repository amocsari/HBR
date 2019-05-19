using System;

namespace Common.Request
{
    public class UpdateBookProgressRequest
    {
        public string BookId { get; set; }
        public int NewProgress { get; set; }
    }
}
