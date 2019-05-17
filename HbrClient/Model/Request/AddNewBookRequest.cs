namespace HbrClient.Model.Request
{
    public class AddNewBookRequest
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PageNumber { get; set; }

        public int? GenreId { get; set; }

        public bool AutoCompleteData { get; set; }

        public byte[] File { get; set; }
    }
}
