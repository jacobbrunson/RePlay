
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
    [Activity(Label = "PromptActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class PromptActivity : Activity
    {
        GridView View;

        List<String> GridViewStrings = new List<String>{"Exercise: Wrist Flexion", "Device: FitMi"};
        List<String> GridViewSubStrings = new List<String> {"Sets: 12 | Repetitions: 2", 
            "For this exercise you will need\n to connect to the FitMi device" };
        List<int> GridViewImages = new List<int> { Resource.Drawable.wrist_flexion_1, Resource.Drawable.FitMi};

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Prompt);

            CustomGridView adapter = new CustomGridView(this, GridViewStrings, GridViewSubStrings, GridViewImages);
            View = FindViewById<GridView>(Resource.Id.grid_view);
            View.Adapter = adapter;
            View.ItemClick += (s, e) =>
			{
                Toast.MakeText(this, "GridView Item: " + GridViewStrings[e.Position], ToastLength.Short).Show();
            };

            this.FindViewById<Button>(Resource.Id.next_button).Click += delegate {
                Intent intent = new Intent(this, typeof(WrapperActivities.PromptActivity));
                StartActivity(intent);
            };

            this.FindViewById<Button>(Resource.Id.cancel_button).Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}
