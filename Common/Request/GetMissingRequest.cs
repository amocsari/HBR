using System;
using System.Collections.Generic;

namespace Common.Request
{
    public class GetMissingRequest : RequestBase
    {
        public List<string> IdList { get; set; } = new List<string>();
    }
}
