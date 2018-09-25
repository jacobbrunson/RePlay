using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace RePlay
{
    [Activity(Label = "RePlay", MainLauncher = true, Icon = "@mipmap/icon")]
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
                Intent intent = new Intent(this, typeof(DummyGame.Android.Activity1));
                intent.PutExtra("CONTENT_DIR", "DummyGame");
                StartActivity(intent);
                button.Text = "Launching game...";
            };
        }
    }
}

