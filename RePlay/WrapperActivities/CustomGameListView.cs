using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace RePlay.WrapperActivities
{
    [Activity(Label = "CustomGameListView")]
    public class CustomGameListView : BaseAdapter
    {
        private Context Context;
        private List<RePlayGame> GamesList;
        private Activity CallerActivity;

        public CustomGameListView(Context mcontext, List<RePlayGame> games)
        {
            Context = mcontext;
            GamesList = games;
            CallerActivity = (GamesListActivity) Context;
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

            //Get the view object for this game
            if (view == null)
            {
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.CustomGamesLauncher, null, false);
            }

            //Set the image/icon that is part of the game view
            ImageView GameView = view.FindViewById<ImageView>(Resource.Id.gameslist_image);
            System.Console.WriteLine(position);
            string drawable_name = GamesList[position].ImageAssetName;
            int my_img_resource;
            try
            {
                my_img_resource = (int)typeof(Resource.Drawable).GetField(drawable_name).GetValue(null);
                GameView.SetImageResource(my_img_resource);
            }
            catch (System.Exception)
            {
                //empty
            }

            //Set the text that shows up as part of the game view
            TextView GameText = view.FindViewById<TextView>(Resource.Id.gameslist_name);
            GameText.Text = GamesList[position].Name;
            if (!GamesList[position].IsGameAvailable)
            {
                GameText.Text += " (Coming soon!)";
            }

            //Define the click behavior for this game view button
            view.Click += (s, e) =>
            {
                if (GamesList[position].AssetNamespace.Equals("Repetitions"))
                {
                    //Create an intent to start the "repetitions mode" activity
                    Intent intent = new Intent(CallerActivity, typeof(DummyGame.Android.Activity1));

                    //Tell the repetitions mode that we want to do 15 reps
                    intent.PutExtra("Repetitions", 2);
                    intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

                    //Start the activity and expect a result to come back
                    CallerActivity.StartActivityForResult(intent, 0);

                }
                else if (GamesList[position].AssetNamespace.Equals("FitMiTraffic"))
                {

                }
                else if (GamesList[position].AssetNamespace.Equals("Breakout"))
                {
                    //Creat an intent to start the "breakout" game
                    Intent intent = new Intent(CallerActivity, typeof(DummyGame.Android.Activity1));

                    //Tell the game to run for 2 minutes
                    intent.PutExtra("Duration", 15);
                    intent.PutExtra("CONTENT_DIR", "blockBreakerAndroid");

                    //Start the activity and expect a result to come back
                    Context.StartActivity(intent);
                }
            };

            //Return the game view to the caller
            return view;
        }
    }
}
