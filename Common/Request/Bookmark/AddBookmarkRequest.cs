namespace Common.Request
{
    public class AddBookmarkRequest : RequestBase
    {
        public string BookmarkId { get; set; }

        public string BookId { get; set; }

        public int PageNumber { get; set; }
    }
}
