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

namespace RePlay.Activities
{
    [Activity(Label = "PrescriptionDoneActivity")]
    public class PrescriptionDoneActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the layout resource
            SetContentView(Resource.Layout.PrescriptionDone);

            ImageButton button = FindViewById<ImageButton>(Resource.Id.games_next);

            button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(GamesListActivity));
                StartActivity(intent);
            };
        }
    }
}