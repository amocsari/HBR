using System.Collections.Generic;

namespace HbrClient.Model.Request
{
    public class GetMissingRequest
    {
        public List<int> IdList { get; set; } = new List<int>();
    }
}
