namespace HbrClient.Model.Dto
{
    public class ClientBookDto : BookDto, IClientDto
    {
        public string UserIdentifier { get; set; } = HbrApplication.UserIdentifier;
    }
}