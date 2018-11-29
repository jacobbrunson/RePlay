using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using RePlay.Activities;
using RePlay.Entity;

namespace RePlay.CustomViews
{
    public class CustomPrescriptionsCardView : BaseAdapter
    {
        List<Prescription> PrescriptionsList;
        readonly Context Context;
        readonly bool ContainsLast;
        readonly bool isAssigned;
        readonly SettingsActivity settingsActivity;

        public CustomPrescriptionsCardView(Context mcontext, List<Prescription> prescriptions, bool hasLastElement)
        {
            Context = mcontext;
            PrescriptionsList = prescriptions;
            ContainsLast = hasLastElement;
            isAssigned = true;
            settingsActivity = (SettingsActivity)mcontext;
        }

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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                if (ContainsLast == true && position == PrescriptionsList.Count - 1)
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.PrescriptionsGridPlus, null, false);
                    ImageView addPrescription = view.FindViewById<ImageButton>(Resource.Id.add_button);
                    addPrescription.Click += Add_Prescription_Click;
                }
                else if (isAssigned)
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.PrescriptionsGrid, null, false);
                    Prescription card = PrescriptionsList[position];
                    TextView ExerciseText = view.FindViewById<TextView>(Resource.Id.exercise_name);
                    ExerciseText.Text = card.Exercise;
                    TextView DeviceText = view.FindViewById<TextView>(Resource.Id.device_name);
                    DeviceText.Text = card.Device;
                    TextView GameText = view.FindViewById<TextView>(Resource.Id.game_name);
                    ImageButton deletePrescriptionButton = view.FindViewById<ImageButton>(Resource.Id.delete_prescription);
                    deletePrescriptionButton.Click += DeletePrescriptionButton_Click;
                    //GameText.Text = card.Game.Name;
                    GameText.Text = card.Game.Name;
                    ImageView PrescriptionImage = view.FindViewById<ImageView>(Resource.Id.prescription_image);
                }
                else
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.SavedPrescription, null, false);
                }
            }

            return view;
        }

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

        void Add_Prescription_Click(object sender, EventArgs e)
        {
            Activity settings = (SettingsActivity)Context;
            FragmentTransaction fm = settings.FragmentManager.BeginTransaction();
            AddPrescriptionFragment dialog = AddPrescriptionFragment.NewInstance(settingsActivity);
            dialog.Show(fm, "dialog fragment");
        }
    }
}