using SQLite;

namespace HbrClient.Model.Dto
{
    public class ClientBookmarkDto : BookmarkDto, IClientEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ClientId { get; set; }
        public bool ModifiedOffline { get; set; }
    }
}