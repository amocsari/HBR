namespace Common.Request
{
    public class AddBookToShelfRequest
    {
        public int BookId { get; set; }
        public int? Progress { get; set; }
    }
}
