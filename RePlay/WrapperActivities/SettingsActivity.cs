using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RePlay
{
    //, ScreenOrientation = ScreenOrientation.Landscape
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);

            /*
             * Future: Create list of image buttons, programmatically displaying each
             * 
             * ImageButton assigned[] = new ImageButton[];
             * 
             * for(ImageButton button : assigned) {
             *      addButtonToLayout(button)
             * }
             * 
             */
//            ImageButton assigned1 = FindViewById<ImageButton>(Resource.Id.reachPic);
//            ImageButton assigned2 = FindViewById<ImageButton>(Resource.Id.flexPic);
//            ImageButton addAssigned = FindViewById<ImageButton>(Resource.Id.plusPic);

//            assigned1.Click += delegate
//            {
//                // toast reach pic
//                Toast.MakeText(this, "Assigned Exercise 1: reach", ToastLength.Long).Show();

//            };

//            assigned2.Click += delegate
//            {
//                // toast wrist flexion pic
//                Toast.MakeText(this, "Assigned Exercise 2: wrist flexion", ToastLength.Long).Show();
//            };

//            addAssigned.Click += delegate
//            {
//                // Create a new fragment and a transaction.
//                // FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
//                // NavigationFragment navBar = new NavigationFragment();

//                // The fragment will have the ID of Resource.Id.fragment_container.
//                // fragmentTx.Add(Resource.Id.fragment_container, navBar);

//                // Commit the transaction.
//                // fragmentTx.Commit();
//                FragmentManager.BeginTransaction();
//                Fragment prev = FragmentManager.FindFragmentByTag("dialog");
//                var prescriptionFragment = AddPrescriptionFragment.NewInstance();
//                prescriptionFragment.Dismissed += (s, e) =>
//                {
//                    var args = (AddPrescriptionFragment.DialogEventArgs)e;
//                    Toast.MakeText(this, String.Format("The Game is {0}.", args.Game), ToastLength.Long).Show();
//                    Toast.MakeText(this, String.Format("The Exercise is {0}.", args.Exercise), ToastLength.Long).Show();
//                    Toast.MakeText(this, String.Format("The Device is {0}.", args.Device), ToastLength.Long).Show();
//                    Toast.MakeText(this, String.Format("The Time is {0}.", args.Time), ToastLength.Long).Show();
//                };
//                prescriptionFragment.Show(FragmentManager, "dialog");
////                FragmentManager.BeginTransaction()
////                               .Add(Android.Resource.Id.Content, prescriptionFragment)
////                               .Commit();
        //    };

        //    ImageButton saved1 = FindViewById<ImageButton>(Resource.Id.curlsPic);
        //    ImageButton saved2 = FindViewById<ImageButton>(Resource.Id.supPic);
        //    ImageButton saved3 = FindViewById<ImageButton>(Resource.Id.typePic);

        //    saved1.Click += delegate
        //    {
        //        // toast curls pic
        //        Toast.MakeText(this, "Saved Exercise 1: bicep curls", ToastLength.Long).Show();
        //    };

        //    saved2.Click += delegate
        //    {
        //        // toast supination pic
        //        Toast.MakeText(this, "Saved Exercise 2: wrist supination", ToastLength.Long).Show();
        //    };
            
        //    saved3.Click += delegate
        //    {
        //        // toast type pic
        //        Toast.MakeText(this, "Saved Exercise 3: typing", ToastLength.Long).Show();
        //    };
        }
    }
}