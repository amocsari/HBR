namespace Common.Request
{
    public class AddOrEditBookRequest
    {
        public string BookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PageNumber { get; set; }

        public string GenreId { get; set; }

        public bool AutoCompleteData { get; set; }
    }
}
