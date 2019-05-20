namespace HbrClient.Model.Dto
{
    public class ClientBookmarkDto : BookmarkDto, IClientDto
    {
        public string UserIdentifier { get; set; } = HbrApplication.UserIdentifier;
    }
}