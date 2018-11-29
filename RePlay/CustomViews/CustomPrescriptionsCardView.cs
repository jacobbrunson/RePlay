using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using RePlay.Activities;
using RePlay.Entity;
using RePlay.Manager;

namespace RePlay.CustomViews
{
    public class CustomPrescriptionsCardView : BaseAdapter
    {
        List<Prescription> PrescriptionsList;
        readonly Context Context;
        readonly bool ContainsLast;
        readonly bool isAssigned;
        readonly SettingsActivity settingsActivity;

        // Class Constructor
        public CustomPrescriptionsCardView(Context mcontext, List<Prescription> prescriptions, bool hasLastElement)
        {
            Context = mcontext;
            PrescriptionsList = prescriptions;
            ContainsLast = hasLastElement;
            isAssigned = true;
            settingsActivity = (SettingsActivity) mcontext;
        }

        // Class Constructor
        public CustomPrescriptionsCardView(Context mcontext, List<Prescription> prescriptions)
        {
            Context = mcontext;
            PrescriptionsList = prescriptions;
            ContainsLast = false;
            isAssigned = false;
        }

        public override int Count
        {
            get { return PrescriptionsList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        // Return view for a custom card
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // Get the view for this prescription; we must determine which layout to use
            if (view == null)
            {
                // Use PrescriptionsGridPlus since this is the 
                if (ContainsLast == true && position == PrescriptionsList.Count - 1)
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.AddPrescriptionCard, null, false);

                    ImageView addPrescription = view.FindViewById<ImageButton>(Resource.Id.add_button);
                    addPrescription.Click += Add_Prescription_Click;
                }
                // Use PrescriptionsGrid layout since this is an assigned prescription
                else if (isAssigned)
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.AssignedPrescriptionCard, null, false);
                    Prescription currentPrescription = PrescriptionsList[position];

                    // Update TextViews on card
                    TextView ExerciseText = view.FindViewById<TextView>(Resource.Id.exercise_name);
                    ExerciseText.Text = currentPrescription.Exercise;

                    TextView DeviceText = view.FindViewById<TextView>(Resource.Id.device_name);
                    DeviceText.Text = currentPrescription.Device;

                    TextView GameText = view.FindViewById<TextView>(Resource.Id.game_name);
                    GameText.Text = currentPrescription.Game.Name;

                    ImageButton deletePrescriptionButton = view.FindViewById<ImageButton>(Resource.Id.delete_prescription);
                    deletePrescriptionButton.Click += DeletePrescriptionButton_Click;

                    ImageView PrescriptionImage = view.FindViewById<ImageView>(Resource.Id.prescription_image);
                    // TODO: update the image based on the exercise name
                }
                // Used SavePrescription layout
                else
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.SavedPrescriptionCard, null, false);
                }
            }

            return view;
        }

        // Handle the event that the delete button is clicked
        void DeletePrescriptionButton_Click(object sender, EventArgs e)
        {
            var prescriptionsPosition = position + SettingsActivity.ItemsPerPage * settingsActivity.ACurrentPage;
            settingsActivity.assigned_paginator.RemoveAt(prescriptionsPosition);
            PrescriptionManager.Instance.SavePrescription();
            settingsActivity.AssignedView.Adapter = new CustomPrescriptionsListView(
                settingsActivity,
                settingsActivity.assigned_paginator.GeneratePage(settingsActivity.ACurrentPage),
                settingsActivity.assigned_paginator.ContainsLast(settingsActivity.ACurrentPage));
        }

        // Handle the event that the add button is clicked
        void Add_Prescription_Click(object sender, EventArgs e)
        {
            // Get a new instance of the AddPrescriptionFragment
            Activity settings = (SettingsActivity)Context;
            FragmentTransaction fm = settings.FragmentManager.BeginTransaction();
            AddPrescriptionFragment dialog = AddPrescriptionFragment.NewInstance(settingsActivity);
            dialog.Show(fm, "dialog fragment");
        }
    }
}