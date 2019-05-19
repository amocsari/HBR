using System.Collections.Generic;

namespace Common.Request
{
    public class GetMissingRequest
    {
        public List<int> IdList { get; set; } = new List<int>();
    }
}
