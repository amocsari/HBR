namespace HbrClient.Model.Request
{
    public class AddBookToShelfRequest
    {
        public int BookId { get; set; }
        public string UserIdentifier { get; set; }
        public int? Progress { get; set; }
    }
}
