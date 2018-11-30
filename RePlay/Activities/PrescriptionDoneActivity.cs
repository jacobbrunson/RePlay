using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace RePlay.Activities
{
    // basic class for 1) notifying patients when they have completed their assigned prescriptions, and
    //                 2) prompting patients to check out the games list page
    [Activity(Label = "PrescriptionDoneActivity")]
    public class PrescriptionDoneActivity : Activity
    {
        // sets up the views and onClick delegate allowing patients to navigate pages
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