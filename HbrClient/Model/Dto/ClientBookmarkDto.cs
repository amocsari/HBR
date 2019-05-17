using SQLite;

namespace HbrClient.Model.Dto
{
    public class ClientBookmarkDto : BookmarkDto
    {
        [PrimaryKey, AutoIncrement]
        public int ClientId { get; set; }
    }
}