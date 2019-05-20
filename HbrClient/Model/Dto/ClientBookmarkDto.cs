namespace HbrClient.Model.Dto
{
    public class ClientBookmarkDto : BookmarkDto
    {
        public string UserIdentifier { get; set; } = HbrApplication.UserIdentifier;
    }
}