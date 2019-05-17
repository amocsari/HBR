using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Request
{
    public class GetMissingRequest
    {
        public int UserId { get; set; }
        public List<int> IdList { get; set; } = new List<int>();
    }
}
