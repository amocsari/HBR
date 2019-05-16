using Android.Views;
using Android.Widget;
using static Android.Support.V7.Widget.RecyclerView;

namespace HbrClient.Bookmark
{
    public class BookmarkViewHolder : ViewHolder
    {
        public TextView PageTextView { get; set; }
        public ImageView DeleteImageView { get; set; }

        public BookmarkViewHolder(View view) : base(view)
        {
            PageTextView = view.FindViewById<TextView>(Resource.Id.textView_bookmark_pageNumber);
            DeleteImageView = view.FindViewById<ImageView>(Resource.Id.image_view_bookmark_delete);
        }
    }
}