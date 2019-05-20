namespace HbrClient.Model.Dto
{
    public class ClientBookDto : BookDto
    {
        public string UserIdentifier { get; set; } = HbrApplication.UserIdentifier;
    }
}