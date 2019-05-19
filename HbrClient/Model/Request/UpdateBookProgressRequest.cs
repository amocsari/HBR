using System;

namespace HbrClient.Model.Request
{
    public class UpdateBookProgressRequest
    {
        public string BookId { get; set; }
        public int NewProgress { get; set; }
    }
}
