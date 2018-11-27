
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
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
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Prompt);


            base.OnCreate(savedInstanceState);
            
            this.FindViewById<ImageButton>(Resource.Id.next).Click += delegate {
                Intent intent = new Intent(this, typeof(WrapperActivities.GamesListActivity));
                StartActivity(intent);
            };

            this.FindViewById<ImageButton>(Resource.Id.cancel).Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }
    }
}
