using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace RePlay
{
    [Activity(Label = "RePlay", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                //Hey look, we can access the DummyGame activity!
                //var gameActivity = new DummyGame.Android.Activity1();
                Intent intent = new Intent(this, typeof(WrapperActivities.GamesListActivity));
                this.StartActivity(intent);
            };
        }
    }
}

