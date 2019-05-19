using System.Collections.Generic;

namespace HbrClient.Model.Request
{
    public class GetMissingRequest
    {
        public List<string> IdList { get; set; } = new List<string>();
    }
}
