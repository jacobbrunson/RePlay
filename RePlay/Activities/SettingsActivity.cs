using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using RePlay.CustomViews;
using RePlay.Entity;
using RePlay.Fragments;
using RePlay.Manager;

namespace RePlay.Activities
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        GridView AssignedView, SavedView;
        ImageButton ALeftButton, ARightButton, SLeftButton, SRightButton;
        ImageView PatientPicture;
        TextView PatientName;

        static List<Prescription> saved = new List<Prescription>() { 
            new Prescription("Bicep Curl", null, "FitMi", 3),
            new Prescription("Bicep Curl", null, "FitMi", 4),
            new Prescription("Bicep Curl", null, "FitMi", 5),
            new Prescription("Bicep Curl", null, "FitMi", 12)};

        const int PRESCRIPTIONS_PER_PAGE = 3;

        AssignedPaginator AssignedPaginator = AssignedPaginator.NewInstance(PRESCRIPTIONS_PER_PAGE, PrescriptionManager.Instance);
        Paginator<Prescription> SavedPaginator = new Paginator<Prescription>(PRESCRIPTIONS_PER_PAGE, saved);

        int ACurrentPage = 0;
        int SCurrentPage = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
            InitializeViews();
            AssignedView.Adapter = new CustomPrescriptionsCardView(this, AssignedPaginator.GeneratePage(ACurrentPage), AssignedPaginator.ContainsLast(ACurrentPage));
            SavedView.Adapter = new CustomPrescriptionsCardView(this, SavedPaginator.GeneratePage(SCurrentPage));
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
            ToggleAButtons();

            ALeftButton.Click += LeftButton_Click_Assigned;
            ARightButton.Click += RightButton_Click_Assigned;

            // Initialize second pair of buttons
            SLeftButton = FindViewById<ImageButton>(Resource.Id.left_button_2);
            SRightButton = FindViewById<ImageButton>(Resource.Id.right_button_2);
            ToggleSButtons();

            SLeftButton.Click += LeftButton_Click_Saved;
            SRightButton.Click += RightButton_Click_Saved;
        }

        void LeftButton_Click_Assigned(object sender, EventArgs e)
        {
            ACurrentPage -= 1;
            AssignedView.Adapter = new CustomPrescriptionsCardView(this, AssignedPaginator.GeneratePage(ACurrentPage), AssignedPaginator.ContainsLast(ACurrentPage));
            ToggleAButtons();
        }

        void RightButton_Click_Assigned(object sender, EventArgs e)
        {
            ACurrentPage += 1;
            AssignedView.Adapter = new CustomPrescriptionsCardView(this, AssignedPaginator.GeneratePage(ACurrentPage), AssignedPaginator.ContainsLast(ACurrentPage));
            ToggleAButtons();
        }

        void LeftButton_Click_Saved(object sender, EventArgs e)
        {
            SCurrentPage -= 1;
            SavedView.Adapter = new CustomPrescriptionsCardView(this, SavedPaginator.GeneratePage(SCurrentPage));
            ToggleSButtons();
        }

        void RightButton_Click_Saved(object sender, EventArgs e)
        {
            SCurrentPage += 1;
            SavedView.Adapter = new CustomPrescriptionsCardView(this, SavedPaginator.GeneratePage(SCurrentPage));
            ToggleSButtons();
        }

        void ToggleAButtons()
        {
            if(ACurrentPage == AssignedPaginator.LastPage && ACurrentPage == 0){
                ALeftButton.Enabled = false;
                ARightButton.Enabled = false;
            }
            else if (ACurrentPage == AssignedPaginator.LastPage)
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
            if (SCurrentPage == SavedPaginator.LastPage && SCurrentPage == 0)
            {
                SLeftButton.Enabled = false;
                SRightButton.Enabled = false;
            }
            else if (SCurrentPage == SavedPaginator.LastPage)
            {
                SLeftButton.Enabled = true;
                SRightButton.Enabled = false;
            }
            else if (SCurrentPage == 0)
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

        // Handle a new assigned prescription added
        public void NewPrescriptionAdded()
        {
            // Create a new assigned paginator object
            AssignedPaginator = AssignedPaginator.NewInstance(PRESCRIPTIONS_PER_PAGE, PrescriptionManager.Instance);
            // Update the adapter
            AssignedView.Adapter = new CustomPrescriptionsCardView(this, AssignedPaginator.GeneratePage(ACurrentPage), AssignedPaginator.ContainsLast(ACurrentPage));
            ToggleAButtons();
        }

        // Handle a prescription deleted
        public void PrescriptionDeleted(int pos)
        {
            int position = pos + PRESCRIPTIONS_PER_PAGE * ACurrentPage;
            if (position == PrescriptionManager.Instance.Count - 1) return;
            AssignedPaginator.RemoveAt(position);
            AssignedPaginator.RemoveAt(PrescriptionManager.Instance.Count - 1);
            PrescriptionManager.Instance.SavePrescription();
            AssignedPaginator = AssignedPaginator.NewInstance(PRESCRIPTIONS_PER_PAGE, PrescriptionManager.Instance);
            AssignedView.Adapter = new CustomPrescriptionsCardView(this, AssignedPaginator.GeneratePage(ACurrentPage), AssignedPaginator.ContainsLast(ACurrentPage));
            ToggleAButtons();
        }
    }
}