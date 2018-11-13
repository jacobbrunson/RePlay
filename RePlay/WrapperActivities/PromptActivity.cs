
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
        // logic
        int index;
        List<Prescription> prescription;

        // ui
        ImageButton next;

        const int REQUEST_CODE = 5432;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Prompt);


            base.OnCreate(savedInstanceState);

            next = this.FindViewById<ImageButton>(Resource.Id.next);

            this.FindViewById<ImageButton>(Resource.Id.cancel).Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            prescription = PrescriptionManager.Instance;
            index = StateManager.Instance.Index;            
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if(requestCode == REQUEST_CODE && resultCode == Result.Ok)
            {
                index++;

                if (index < prescription.Count)
                {
                    UpdateView();
                    StateManager.Instance.UpdateState(DateTimeOffset.Now.ToUnixTimeMilliseconds(), index);
                    // load prescription[i].view
                    next.Click += delegate
                    {
                        // Intent intent = new Intent(this, typeof(prescription[i].Game));
                        Intent intent = new Intent(this, typeof(WrapperActivities.GamesListActivity));
                        intent.PutExtra("exercise", prescription[index].Exercise);
                        intent.PutExtra("duration", prescription[index].Duration);
                        StartActivityForResult(intent, REQUEST_CODE);
                    };
                }
                else
                {
                    //Intent intent = new Intent(this, typeof(PrescriptionDoneActivity));
                    //StartActivity(intent);
                }
            }
        }

        // update the views pictures based on prescription[index]
        public void UpdateView()
        {

        }
    }
}
