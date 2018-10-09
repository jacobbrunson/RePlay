
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
    [Activity(Label = "GamesListActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class GamesListActivity : Activity
    {
        GridView View;
        List<Game> GamesList = new List<Game> { new Game(0, "Breakout"), new Game(1, "Traffic Racer"), 
            new Game(2, "Fruit Archery"), new Game(3, "Temple Run"), new Game(4, "Crossy Road"),
            new Game(5, "Typer Shark")};

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GamesList);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.gameslist_sort_dropdown);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.sortby_options, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            CustomGameListView GameAdapter = new CustomGameListView(this, GamesList);
            View = FindViewById<GridView>(Resource.Id.gameslist_grid);
            View.Adapter = GameAdapter;
            View.ItemClick += (s, e) =>
            {
                Toast.MakeText(this, "GridView Item: " + GamesList[e.Position], ToastLength.Short).Show();

                Intent intent = new Intent(this, typeof(DummyGame.Android.Activity1));
                intent.PutExtra("CONTENT_DIR", "DummyGame");
                StartActivity(intent);
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The sort option is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}
