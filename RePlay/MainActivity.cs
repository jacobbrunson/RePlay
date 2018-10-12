using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;

namespace RePlay
{
    [Activity(Label = "RePlay", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Load games from games.txt
            LoadGames();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            ImageButton button = FindViewById<ImageButton>(Resource.Id.playButton);

            button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(WrapperActivities.PromptActivity));
                StartActivity(intent);
            };
        }

        private void LoadGames() {
            GameManager.Instance.LoadGames(Assets);
        }
    }
}