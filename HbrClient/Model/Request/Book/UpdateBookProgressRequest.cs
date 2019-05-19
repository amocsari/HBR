using System;

namespace HbrClient.Model.Request
{
    public class UpdateBookProgressRequest : RequestBase
    {
        public string BookId { get; set; }
        public int NewProgress { get; set; }
    }
}
