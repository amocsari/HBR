namespace HbrClient.Model.Request
{
    public class AddBookToShelfRequest : RequestBase
    {
        public string BookId { get; set; }
        public int? Progress { get; set; }
    }
}
