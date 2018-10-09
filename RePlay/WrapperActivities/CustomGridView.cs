
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RePlay.WrapperActivities
{
    [Activity(Label = "CustomGridView")]
    public class CustomGridView : BaseAdapter
    {
        private Context Context;
        private List<String> GridViewStrings;
        private List<String> GridViewSubStrings;
        private List<int> GridViewImages;

        public CustomGridView(Context mcontext, List<String> strings, List<String> sub, List<int> images){
            Context = mcontext;
            GridViewStrings = strings;
            GridViewSubStrings = sub;
            GridViewImages = images;
        }

        public override int Count
        {
            get { return GridViewStrings.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.GridViewLayout, null, false);
            }
            TextView main = view.FindViewById<TextView>(Resource.Id.gridview_main_text);
            main.Text = GridViewStrings[position];

            TextView sub = view.FindViewById<TextView>(Resource.Id.gridview_sub_text);
            sub.Text = GridViewSubStrings[position];

            ImageButton button = view.FindViewById<ImageButton>(Resource.Id.gridview_image);
            button.SetImageResource(GridViewImages[position]);

            return view;
        }
    }
}
