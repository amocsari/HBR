namespace HbrClient.Model.Request
{
    public class AddBookToShelfRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int? Progress { get; set; }
    }
}
