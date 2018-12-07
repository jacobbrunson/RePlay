using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using RePlay.Entity;
using RePlay.Manager;

namespace RePlay.Activities
{
    // an activity designed to describe the current exercise and game a patient needs to complete for his/her prescription
    [Activity(Label = "PromptActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class PromptActivity : Activity
    {
        // logic
        int index;
        List<Prescription> prescription;
        ExerciseManager exercises;
        ActivityLogManager log;

        // ui
        ImageButton next;
        ImageView exercisePic, devicePic;
        TextView exerciseText, gameText;

        const int REQUEST_CODE = 5432;

        // sets up the UI and gets the current prescription and patient progress (state) from the
        // relevant manager classes
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Prompt);


            base.OnCreate(savedInstanceState);

            
            // ui elements
            gameText = FindViewById<TextView>(Resource.Id.prompt_game_text);
            exercisePic = FindViewById<ImageView>(Resource.Id.prompt_exercise_image);
            exerciseText = FindViewById<TextView>(Resource.Id.prompt_exercise_text);
            devicePic = FindViewById<ImageView>(Resource.Id.prompt_device);
            next = FindViewById<ImageButton>(Resource.Id.next);

            FindViewById<ImageButton>(Resource.Id.cancel).Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            // managers
            prescription = PrescriptionManager.Instance;
            index = StateManager.Instance.Index;
            exercises = ExerciseManager.Instance;
            log = ActivityLogManager.Instance;
            
            UpdateState();
        }

        // loads when a game returns a result (after a StartActivityForResult)
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if(requestCode == REQUEST_CODE && resultCode == Result.Ok)
            {
                log.SaveActivity(
                    prescription[index].Exercise,
                    prescription[index].Game.Name, 
                    "prescription");

                index++;
                UpdateState();
            }
        }
        
        // update the ui and onClick bindings to allow the next exercise in the patient's
        // prescription to be run; also responsible for letting the patient know when
        // they have completed their prescription in its entirety
        public void UpdateState()
        {
            if (index < prescription.Count)
            {
                UpdateView();
                StateManager.Instance.UpdateState(DateTimeOffset.Now.ToUnixTimeMilliseconds(), index);
                
                // load prescription[i].view
                next.Click += delegate
                {
                    RePlayGame game = prescription[index].Game;
                    Type t = Type.GetType(game.AssemblyQualifiedName); //This is what gets the correct name
                    Intent intent = new Intent(this, t);


                    intent.PutExtra("CONTENT_DIR", game.AssetNamespace); //Correct asset namespace
                    intent.PutExtra("exercise", prescription[index].Exercise);
                    intent.PutExtra("duration", prescription[index].Duration);
                    StartActivityForResult(intent, REQUEST_CODE);
                };
            }
            else
            {
                // set index to 0 so patient can run through prescription again
                index = 0;
                StateManager.Instance.UpdateState(DateTimeOffset.Now.ToUnixTimeMilliseconds(), index);

                // go to prescriptions finished page - located in branch patrick/exercises
                Intent intent = new Intent(this, typeof(PrescriptionDoneActivity));
                StartActivity(intent);
            }
        }

        // update the exercise image, exercise name, and game description based on the current index
        void UpdateView()
        {
            gameText.Text = prescription[index].Game.Name;
            exerciseText.Text = CapitalizeFirst(prescription[index].Exercise);
            exercisePic.SetImageResource(exercises.MapNameToPic(prescription[index].Exercise, this));
        }

        // utility method to return the input argument with the first letter in every word capitalized
        public string CapitalizeFirst(String text)
        {
            return string.Join(" ", text.Split().Select(x => x.Substring(0, 1).ToUpper() + x.Substring(1)));
        }
    }
}
