
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
using Java.Lang;

namespace RePlay.WrapperActivities
{
    [Activity(Label = "CustomGameListView")]
    public class CustomGameListView : BaseAdapter
    {
        private Context Context;
        private List<RePlayGame> GamesList;

        public CustomGameListView(Context mcontext, List<RePlayGame> games)
        {
            Context = mcontext;
            GamesList = games;
        }

        public override int Count
        {
            get { return GamesList.Count; }
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
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.CustomGamesLauncher, null, false);
            }

            ImageView GameView = view.FindViewById<ImageView>(Resource.Id.gameslist_image);
            //GameView.SetImageResource(GamesList[position].Image);

            TextView GameText = view.FindViewById<TextView>(Resource.Id.gameslist_name);
            GameText.Text = GamesList[position].Name;

            return view;
        }
    }

    public class Game{

        public int Image{
            get;
            set;
        }

        public string Name{
            get;
            set;
        }

        public Game(int img, string n){
            Image = img;
            Name = n;
        }
    }
}
