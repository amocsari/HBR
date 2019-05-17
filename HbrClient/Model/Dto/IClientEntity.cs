namespace HbrClient.Model.Dto
{
    public interface IClientEntity
    {
        int ClientId { get; set; }
        bool ModifiedOffline { get; set; }
    }
}