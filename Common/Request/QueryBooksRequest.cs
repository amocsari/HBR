namespace Common.Request
{
    public class QueryBooksRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int? Genre { get; set; }
    }
}
