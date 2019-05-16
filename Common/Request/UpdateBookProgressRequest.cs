namespace Common.Request
{
    public class UpdateBookProgressRequest
    {
        public int BookId { get; set; }
        public int NewProgress { get; set; }
    }
}
