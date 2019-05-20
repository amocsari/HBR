using Android.Views;
using Android.Widget;
using static Android.Support.V7.Widget.RecyclerView;

namespace HbrClient.BookQuery
{
    class QueryBookViewHolder : ViewHolder
    {
        public TextView TitleTextView { get; set; }
        public TextView AuthorTextView { get; set; }

        public QueryBookViewHolder(View view) : base(view)
        {
            TitleTextView = view.FindViewById<TextView>(Resource.Id.tv_title);
            AuthorTextView = view.FindViewById<TextView>(Resource.Id.tv_author);
        }
    }
}