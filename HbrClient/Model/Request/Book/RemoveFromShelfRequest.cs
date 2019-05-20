using System;
using System.Collections.Generic;
using System.Text;

namespace HbrClient.Model.Request
{
    public class RemoveFromShelfRequest : RequestBase
    {
        public string BookId { get; set; }
    }
}
