﻿
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
        // logic
        int index;
        List<Prescription> prescription;

        // ui
        ImageButton next;
        ImageView exercisePic, devicePic;
        TextView exerciseText;

        const int REQUEST_CODE = 5432;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Prompt);


            base.OnCreate(savedInstanceState);

            next = this.FindViewById<ImageButton>(Resource.Id.next);
            exercisePic = this.FindViewById<ImageView>(Resource.Id.prompt_exercise_image);
            exerciseText = this.FindViewById<TextView>(Resource.Id.prompt_exercise_text);
            devicePic = this.FindViewById<ImageView>(Resource.Id.prompt_device);

            this.FindViewById<ImageButton>(Resource.Id.cancel).Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            prescription = PrescriptionManager.Instance;
            index = StateManager.Instance.Index;
            UpdateState();
        }

        // loads when a game returns a result (after a StartActivityForResult)
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if(requestCode == REQUEST_CODE && resultCode == Result.Ok)
            {
                index++;
                UpdateState();
            }
        }

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
                    //Intent intent = new Intent(this, t);


                    Intent intent = new Intent(this, typeof(WrapperActivities.GamesListActivity));
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

                // go to prescriptions finished page - located in branch patrick/exercises
                //Intent intent = new Intent(this, typeof(PrescriptionDoneActivity));
                //StartActivity(intent);
            }
        }

        // update the views pictures based on prescription[index]
        private void UpdateView()
        {
            exerciseText.Text = CapitalizeFirst(prescription[index].Exercise);
            exercisePic.SetImageResource(MapNameToPic(prescription[index].Exercise));
        }

        public string CapitalizeFirst(String text)
        {
            return string.Join(" ", text.Split().Select(x => x.Substring(0, 1).ToUpper() + x.Substring(1)));
        }

        private int MapNameToPic(string exercise)
        {
            switch(exercise)
            {
                case "wrist flexion":
                    return Resource.Drawable.wristflex0;
                default:
                    return Resource.Drawable.curls0;
            }
        }
    }
}
