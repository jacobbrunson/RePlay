
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RePlay
{
    public class AddExerciseFragment : DialogFragment
    {
        public static AddExerciseFragment NewInstance()
        {
            var dialogFragment = new AddExerciseFragment();
            return dialogFragment;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var builder = new AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;
            var dialogView = inflater.Inflate(Resource.Layout.AddExercise, null);

            if (dialogView != null)
            {

                builder.SetView(dialogView);
                builder.SetPositiveButton("Cancel", CancelClicked);
                builder.SetNegativeButton("Add", AddClicked);
            }

            var dialog = builder.Create();
            return dialog;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Toast.MakeText(GetActivity(), "Device Spinner clicked", ToastLength.Short);
        }

        private void AddClicked(object sender, EventArgs eventArgs)
        {

            Toast.MakeText(GetActivity(), "Device Spinner clicked", ToastLength.Short);
        }

        //        public override void OnCreate(Bundle savedInstanceState)
        //        {
        //            base.OnCreate(savedInstanceState);
        //
        //            // Create your fragment here
        //        }

        private void homeClicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            Intent intent = new Intent(button.Context, typeof(MainActivity));
            button.Context.StartActivity(intent);
        }

        private void exerciseSpinnerClicked(object sender, EventArgs e)
        {
            Toast.MakeText(Activity.ApplicationContext, "Exercise Spinner clicked", ToastLength.Short);
        }

        private void gameSpinnerClicked(object sender, EventArgs e)
        {
            Toast.MakeText(Activity.ApplicationContext, "Game Spinner clicked", ToastLength.Short);
        }

        private void deviceSpinnerClicked(object sender, EventArgs e)
        {
            Toast.MakeText(Activity.ApplicationContext, "Device Spinner clicked", ToastLength.Short);
        }

        private void timeSpinnerClicked(object sender, EventArgs e)
        {
            Toast.MakeText(Activity.ApplicationContext, "Time Spinner clicked", ToastLength.Short);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddExercise, container, false);
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view.FindViewById<Spinner>(Resource.Id.exercise_text_spinner).Click += exerciseSpinnerClicked;
            view.FindViewById<Spinner>(Resource.Id.game_text_spinner).Click += gameSpinnerClicked;
            view.FindViewById<Spinner>(Resource.Id.device_text_spinner).Click += deviceSpinnerClicked;
            view.FindViewById<Spinner>(Resource.Id.time_text_spinner).Click += timeSpinnerClicked;
            return view;
        }
    }
}