using System.Collections.Generic;

namespace HbrClient.Model.Request
{
    public class GetMissingRequest
    {
        public int UserId { get; set; }
        public List<int> IdList { get; set; } = new List<int>();
    }
}
