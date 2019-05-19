using System;

namespace Common.Request
{
    public class UpdateBookProgressRequest : RequestBase
    {
        public string BookId { get; set; }
        public int NewProgress { get; set; }
    }
}
