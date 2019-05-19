using System.Collections.Generic;

namespace HbrClient.Model.Response
{
    public class GetMissingResponse<T>
    {
        public List<T> RemoteDtoList { get; set; }
        public List<string> MissingIdList { get; set; }
    }
}
