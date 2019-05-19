using SQLite;

namespace HbrClient.Model.Dto
{
    public class ClientBookmarkDto : BookmarkDto, IClientEntity
    {
        public bool ModifiedOffline { get; set; }
    }
}