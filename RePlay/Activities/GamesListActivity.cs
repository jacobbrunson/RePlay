using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using RePlay.CustomViews;
using RePlay.Entity;
using RePlay.Manager;

// GamesListActivity: Select a game from a grid of all available games
namespace RePlay.Activities
{
    [Activity(Label = "GamesListActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class GamesListActivity : Activity
    {
        GridView View;
        ImageButton LeftButton, RightButton;
        static readonly List<RePlayGame> games = GameManager.Instance;
        Paginator<RePlayGame> p = new Paginator<RePlayGame>(6, games);
        int CurrentPage = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GameList);
            InitializeViews();
            View.Adapter = new CustomGameCardView(this, p.GeneratePage(CurrentPage));
        }

        // Instantiate the left and right buttons
        void InitializeViews()
        {
            View = FindViewById<GridView>(Resource.Id.gameslist_grid);
            LeftButton = FindViewById<ImageButton>(Resource.Id.gameslist_left);
            RightButton = FindViewById<ImageButton>(Resource.Id.gameslist_right);
            LeftButton.Enabled = false;

            LeftButton.Click += LeftButton_Click;
            RightButton.Click += RightButton_Click;

        }

        // Handle a left button click; go back a page
        void LeftButton_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            View.Adapter = new CustomGameCardView(this, p.GeneratePage(CurrentPage));
            ToggleButtons();
        }

        // Handle a right button click; go to next page
        void RightButton_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            View.Adapter = new CustomGameCardView(this, p.GeneratePage(CurrentPage));
            ToggleButtons();
        }

        // Disable buttons according to current page number
        void ToggleButtons()
        {
            // Disable right button on last page
            if (CurrentPage == p.LastPage)
            {
                LeftButton.Enabled = true;
                RightButton.Enabled = false;
            }
            // Disable left button on first page
            else if (CurrentPage == 0)
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
                //Intent intent = new Intent(this, typeof(Type.GetType()));
                //intent.PutExtra("CONTENT_DIR", AssetNamespace);

                /*
                 * 
                 * intent.PutExtra("type", Prescription.exercise);  // if not a prescription, choose an exercise string
                 *
                 * used in Game class:
                 * FitMiExerciseType type = (FitMiExerciseType) Enum.Parse(typeof(FitMiExerciseType), Intent.GetExtraString("type"));
                 * if(typeSupported(type)
                 *      FitMiExerciseBase exercise = FitMiExerciseBase.GetExerciseClass(new HIDPuckDongle(this), FitMiExerciseType.Curls);
                 * else 
                 *      FitMiExerciseBase exercise = FitMiExerciseBase.GetExerciseClass(new HIDPuckDongle(this), defaultExercise);
                 */


                //StartActivity(intent);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            Console.WriteLine("game finished!!!");
        }
    }
}
