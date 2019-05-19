using System;
using System.Collections.Generic;

namespace Common.Request
{
    public class GetMissingRequest
    {
        public List<string> IdList { get; set; } = new List<string>();
    }
}
