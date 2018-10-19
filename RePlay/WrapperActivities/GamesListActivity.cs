
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
        ImageButton LeftButton, RightButton;
        Paginator<RePlayGame> p = new Paginator<RePlayGame>(6, new List<RePlayGame>());
        int TotalPages = Paginator<RePlayGame>.TOTAL_NUM_ITEMS / Paginator<RePlayGame>.ITEMS_PER_PAGE;
        int CurrentPage = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GamesLauncher);
            this.InitializeViews();
            View.Adapter = new CustomGameListView(this, p.GeneratePage(CurrentPage));
        }

        private void InitializeViews()
        {
            View = FindViewById<GridView>(Resource.Id.gameslist_grid);
            LeftButton = FindViewById<ImageButton>(Resource.Id.gameslist_left);
            RightButton = FindViewById<ImageButton>(Resource.Id.gameslist_right);
            LeftButton.Enabled = false;

            LeftButton.Click += LeftButton_Click;
            RightButton.Click += RightButton_Click;

        }

        void LeftButton_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            View.Adapter = new CustomGameListView(this, p.GeneratePage(CurrentPage));
            ToggleButtons();
        }

        void RightButton_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            View.Adapter = new CustomGameListView(this, p.GeneratePage(CurrentPage));
            ToggleButtons();
        }

        private void ToggleButtons()
        {
            if(CurrentPage == TotalPages)
            {
                LeftButton.Enabled = true;
                RightButton.Enabled = false;
            }
            else if(CurrentPage == 0)
            {
                LeftButton.Enabled = false;
                RightButton.Enabled = true;
            }
            else
            {
                LeftButton.Enabled = true;
                RightButton.Enabled = true;
            }

            View.ItemClick += (s, e) =>
            {
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
