using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using RePlay.Activities;
using RePlay.Entity;
using RePlay.Fragments;
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
            settingsActivity = (SettingsActivity)mcontext;
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
                    deletePrescriptionButton.Click += (sender, args) =>
                    {
                        AlertDialog.Builder dialog = new AlertDialog.Builder(settingsActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Confirm");
                        alert.SetMessage("Are you sure you want to remove this prescription?");
                        alert.SetButton("YES", (c, ev) =>
                        {
                            settingsActivity.PrescriptionDeleted(position);
                        });
                        alert.SetButton2("NO", (c, ev) => {
                            alert.Dismiss();
                        });
                        alert.Show();
                    };

                    ImageButton SaveAssigned = view.FindViewById<ImageButton>(Resource.Id.save_assigned_exercise);
                    SaveAssigned.Click += (sender, args) =>
                    {
                        AlertDialog.Builder dialog = new AlertDialog.Builder(settingsActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Confirm");
                        alert.SetMessage("Are you sure you want to save this prescription?");
                        alert.SetButton("YES", (c, ev) =>
                        {
                            if (!SavedPrescriptionManager.Instance.Contains(currentPrescription))
                            {
                                SavedPrescriptionManager.Instance.Add(currentPrescription);
                                SavedPrescriptionManager.Instance.SavePrescription();
                                settingsActivity.RefreshSavedPrescriptions();
                            }
                            else Toast.MakeText(Context, "This prescription has already been saved!", ToastLength.Short).Show();
                        });
                        alert.SetButton2("NO", (c, ev) => {
                            alert.Dismiss();
                        });
                        alert.Show();
                    };

                    ImageView PrescriptionImage = view.FindViewById<ImageView>(Resource.Id.prescription_image);
                    PrescriptionImage.SetImageResource(ExerciseManager.Instance.MapNameToPic(currentPrescription.Exercise, (Activity)Context));
                }

                // Used SavePrescription layout
                else
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.SavedPrescriptionCard, null, false);
                    Prescription currentPrescription = PrescriptionsList[position];

                    TextView ExerciseText = view.FindViewById<TextView>(Resource.Id.exercise_name);
                    ExerciseText.Text = currentPrescription.Exercise;

                    TextView GameText = view.FindViewById<TextView>(Resource.Id.game_name);
                    GameText.Text = currentPrescription.Game.Name;

                    ImageButton DeletePrescriptionButton = view.FindViewById<ImageButton>(Resource.Id.delete_image);
                    DeletePrescriptionButton.Click += (sender, args) =>
                    {
                        AlertDialog.Builder dialog = new AlertDialog.Builder(settingsActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Confirm");
                        alert.SetMessage("Are you sure you want to delete this prescription?");
                        alert.SetButton("YES", (c, ev) =>
                        {
                            SavedPrescriptionManager.Instance.RemoveAt(position);
                            SavedPrescriptionManager.Instance.SavePrescription();
                            settingsActivity.RefreshSavedPrescriptions();
                        });
                        alert.SetButton2("NO", (c, ev) => {
                            alert.Dismiss();
                        });
                        alert.Show();
                    };

                    ImageButton AssignPrescription = view.FindViewById<ImageButton>(Resource.Id.check_image);
                    AssignPrescription.Click += (sender, e) =>
                    {
                        AlertDialog.Builder dialog = new AlertDialog.Builder(settingsActivity);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Confirm");
                        alert.SetMessage("Do you want to assign this prescription?");
                        alert.SetButton("YES", (c, ev) =>
                        {
                            if(!PrescriptionManager.Instance.Contains(currentPrescription)){
                                PrescriptionManager.Instance.RemoveAt(PrescriptionManager.Instance.Count - 1);
                                PrescriptionManager.Instance.Add(currentPrescription);

                                PrescriptionManager.Instance.SavePrescription();
                                settingsActivity.NewPrescriptionAdded();
                            }
                            else Toast.MakeText(Context, "This prescription is already assigned!", ToastLength.Short).Show();
                        });
                        alert.SetButton2("NO", (c, ev) => {
                            alert.Dismiss();
                        });
                        alert.Show();
                    };
                    ImageView PrescriptionImage = view.FindViewById<ImageView>(Resource.Id.exercise_image);
                    PrescriptionImage.SetImageResource(ExerciseManager.Instance.MapNameToPic(currentPrescription.Exercise, (Activity)Context));
                }
            }

            return view;
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