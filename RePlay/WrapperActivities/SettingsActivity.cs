using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RePlay.WrapperActivities
{
    [Activity(Label = "Settings")]
    public partial class SettingsActivity : Activity
    {
        GridView AssignedView, SavedView;
        ImageButton ALeftButton, ARightButton, SLeftButton, SRightButton;
        ImageView PatientPicture;
        TextView PatientName;

        static List<Prescription> saved = new List<Prescription> {
            //new Prescription("Bicep Curl", null, "FitMi", 3),
            //new Prescription("Wrist Supination", null, "FitMi", 3),
            //new Prescription("Left-to-Right", null, "FitMi", 3),
            //new Prescription("Typing", null, "FitMi", 3)
        };

        const int ItemsPerPage = 3;

        Paginator<Prescription> assigned_paginator = new Paginator<Prescription>(ItemsPerPage, PrescriptionManager.Instance);
        Paginator<Prescription> saved_paginator = new Paginator<Prescription>(ItemsPerPage, saved);

        int ACurrentPage = 0;
        int SCurrentPage = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
            InitializeViews();
            AssignedView.Adapter = new CustomPrescriptionsListView(this, assigned_paginator.GeneratePage(ACurrentPage), assigned_paginator.ContainsLast(ACurrentPage));
            SavedView.Adapter = new CustomPrescriptionsListView(this, saved_paginator.GeneratePage(SCurrentPage));
            PatientPicture = FindViewById<ImageView>(Resource.Id.settings_picture);
            PatientPicture.Click += PatientPicture_Click;
            PatientName = FindViewById<TextView>(Resource.Id.therapist_name);
        }

        void InitializeViews()
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

        void ToggleAButtons()
        {
            if (ACurrentPage == assigned_paginator.LastPage)
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

        void ToggleSButtons()
        {
            if (SCurrentPage == saved_paginator.LastPage)
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

        void PatientPicture_Click(object sender, EventArgs e)
        {
            string name = PatientName.Text;
            Activity settings = this;
            FragmentTransaction fm = settings.FragmentManager.BeginTransaction();
            PatientFragment dialog = PatientFragment.NewInstance(name);
            PatientFragment.DialogClosed += OnDialogClosed;
            dialog.Show(fm, "dialog fragment");
        }

        void OnDialogClosed(object sender, string e)
        {
            PatientName.Text = e;
        }
    }
}