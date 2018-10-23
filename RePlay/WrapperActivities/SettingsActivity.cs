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
using RePlay.WrapperActivities;

namespace RePlay.WrapperActivities
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        GridView AssignedView, SavedView;
        ImageButton ALeftButton, ARightButton, SLeftButton, SRightButton;

        static List<SettingsPrescription> assigned = new List<SettingsPrescription> {
            new SettingsPrescription(0, "Breakout", "Left-to-Right", "FitMi", 3), 
            new SettingsPrescription(0, "Temple Run", "WristFlexion", "FitMi", 3),
            new SettingsPrescription(0, "Crossy Road", "Bicep Curl", "FitMi", 3),
            new SettingsPrescription(0, "Handwriting", "Thumb Press", "FitMi", 3)
        };

        static List<SettingsPrescription> saved = new List<SettingsPrescription> {
            new SettingsPrescription(0, "Traffic Racer", "Bicep Curl", "FitMi", 3),
            new SettingsPrescription(0, "Breakout", "Wrist Supination", "FitMi", 3),
            new SettingsPrescription(0, "Temple Run", "Left-to-Right", "FitMi", 3),
            new SettingsPrescription(0, "Typer Shark", "Typing", "FitMi", 3)
        };

        Paginator<SettingsPrescription> assigned_paginator = new Paginator<SettingsPrescription>(3, assigned);
        Paginator<SettingsPrescription> saved_paginator = new Paginator<SettingsPrescription>(3, saved);

        int ACurrentPage = 0;
        int SCurrentPage = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
            this.InitializeViews();
            AssignedView.Adapter = new CustomPrescriptionsListView(this, assigned_paginator.GeneratePage(ACurrentPage), assigned_paginator.ContainsLast(ACurrentPage),
                                                                   (view, str) => {
                Console.WriteLine("Hello world");
                if (string.Compare("last", str, StringComparison.CurrentCulture) == 0) {
                    System.Console.WriteLine(string.Format("{0}", view.GetType()));
                    ImageButton button = view.FindViewById<ImageButton>(Resource.Id.add_button);
                    button.Click += (obj, args) =>
                    {

                        FragmentManager.BeginTransaction();
                        Fragment prev = FragmentManager.FindFragmentByTag("dialog");
                        var prescriptionFragment = AddPrescriptionFragment.NewInstance();
                        prescriptionFragment.Dismissed += (s, e) =>
                        {
                            var _args = (AddPrescriptionFragment.DialogEventArgs)e;
                            Toast.MakeText(this, String.Format("The Game is {0}.", _args.Game), ToastLength.Long).Show();
                            Toast.MakeText(this, String.Format("The Exercise is {0}.", _args.Exercise), ToastLength.Long).Show();
                            Toast.MakeText(this, String.Format("The Device is {0}.", _args.Device), ToastLength.Long).Show();
                            Toast.MakeText(this, String.Format("The Time is {0}.", _args.Time), ToastLength.Long).Show();
                            };
                            prescriptionFragment.Show(FragmentManager, "dialog");
                            FragmentManager.BeginTransaction()
                            .Add(Android.Resource.Id.Content, prescriptionFragment)
                            .Commit();
                        };
                    }
                });
            SavedView.Adapter = new CustomPrescriptionsListView(this, saved_paginator.GeneratePage(SCurrentPage));

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

        }

        private void InitializeViews()
        {   
            // Initialize both grids
            AssignedView = FindViewById<GridView>(Resource.Id.settings_gridview_1);
            SavedView = FindViewById<GridView>(Resource.Id.settings_gridview_2);

            // Initialize first pair of buttons
            ALeftButton = FindViewById<ImageButton>(Resource.Id.left_button_1);
            ARightButton = FindViewById<ImageButton>(Resource.Id.right_button_1);
            ALeftButton.Enabled = false;

            ALeftButton.Click += LeftButton_Click_Assigned;
            ARightButton.Click += RightButton_Click_Assigned;

            // Initialize second pair of buttons
            SLeftButton = FindViewById<ImageButton>(Resource.Id.left_button_2);
            SRightButton = FindViewById<ImageButton>(Resource.Id.right_button_2);
            SLeftButton.Enabled = false;

            SLeftButton.Click += LeftButton_Click_Saved;
            SRightButton.Click += RightButton_Click_Saved;

        }

        void LeftButton_Click_Assigned(object sender, EventArgs e)
        {
            ACurrentPage -= 1;
            AssignedView.Adapter = new CustomPrescriptionsListView(this, assigned_paginator.GeneratePage(ACurrentPage), assigned_paginator.ContainsLast(ACurrentPage));
            ToggleAButtons();
        }

        void RightButton_Click_Assigned(object sender, EventArgs e)
        {
            ACurrentPage += 1;
            AssignedView.Adapter = new CustomPrescriptionsListView(this, assigned_paginator.GeneratePage(ACurrentPage), assigned_paginator.ContainsLast(ACurrentPage));
            ToggleAButtons();
        }

        void LeftButton_Click_Saved(object sender, EventArgs e)
        {
            SCurrentPage -= 1;
            SavedView.Adapter = new CustomPrescriptionsListView(this, saved_paginator.GeneratePage(SCurrentPage));
            ToggleSButtons();
        }

        void RightButton_Click_Saved(object sender, EventArgs e)
        {
            SCurrentPage += 1;
            SavedView.Adapter = new CustomPrescriptionsListView(this, saved_paginator.GeneratePage(SCurrentPage));
            ToggleSButtons();
        }

        private void ToggleAButtons()
        {
            if (ACurrentPage == assigned_paginator.LAST_PAGE)
            {
                ALeftButton.Enabled = true;
                ARightButton.Enabled = false;
            }
            else if (ACurrentPage == 0)
            {
                ALeftButton.Enabled = false;
                ARightButton.Enabled = true;
            }
            else
            {
                ALeftButton.Enabled = true;
                ARightButton.Enabled = true;
            }
        }

        private void ToggleSButtons()
        {
            if (SCurrentPage == saved_paginator.LAST_PAGE)
            {
                SLeftButton.Enabled = true;
                SRightButton.Enabled = false;
            }
            else if (ACurrentPage == 0)
            {
                SLeftButton.Enabled = false;
                SRightButton.Enabled = true;
            }
            else
            {
                SLeftButton.Enabled = true;
                SRightButton.Enabled = true;
            }
        }
    }
}