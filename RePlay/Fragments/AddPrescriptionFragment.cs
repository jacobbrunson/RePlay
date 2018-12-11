using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using RePlay.Manager;
using RePlay.Entity;
using RePlay.Activities;
using System.Linq;
using System;

namespace RePlay.Fragments
{
    // Class for add presription dialog that
    // pops open when the therapist adds a
    // new prescription the settings page.
    public class AddPrescriptionFragment : DialogFragment
    {
        readonly SettingsActivity settingsActivity;
        readonly List<string> DevicesList = new List<string>() { "FitMi", "Knob sensor" };
        readonly int []values = { 30, 60, 90, 120, 180, 240, 300 };

        // Constructor for AddPrescriptionFragment
        // Takes an argument for an instance to
        // SettingsActivity to be able to reference
        // SettingsActivity's variables
        public AddPrescriptionFragment(SettingsActivity settingsActivity)
        {
            this.settingsActivity = settingsActivity;
        }

        // Return a new instance of this class
        public static AddPrescriptionFragment NewInstance(SettingsActivity settingsActivity)
        {
            var dialogFragment = new AddPrescriptionFragment(settingsActivity);
            return dialogFragment;
        }

        // Inflates the view for AddPrescriptionFragment
        // and sets event handlers for the various
        // elements of this dialog
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Begin building a new dialog.
            var builder = new AlertDialog.Builder(Activity);

            //Get the layout inflater
            var inflater = Activity.LayoutInflater;

            //Inflate the layout for this dialog
            var dialogView = inflater.Inflate(Resource.Layout.AddPrescriptionFragment, null);

            if (dialogView != null)
            {
                Spinner gameSpinner = dialogView.FindViewById<Spinner>(Resource.Id.gameSpinner);
                Spinner exerciseSpinner = dialogView.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                NumberPicker timeNumberPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.timeNumberPicker);

                timeNumberPicker.MinValue = 0;
                timeNumberPicker.MaxValue = values.Length-1;
                timeNumberPicker.Value = 0;
                timeNumberPicker.SetDisplayedValues(values.Select(value => "" + value).ToArray());
                timeNumberPicker.WrapSelectorWheel = true;

                Button cancelButton = dialogView.FindViewById<Button>(Resource.Id.cancelButton);
                Button addButton = dialogView.FindViewById<Button>(Resource.Id.addButton);

                cancelButton.Click += CancelButton_Click;
                // We add an anonymous method to the
                // `addButton.Click` delegate
                addButton.Click += (sender, args) =>
                {
                    // Retrive the actual RePlayGame object
                    // with the same game name as the one the
                    // user picked.
                    RePlayGame game = GameManager.Instance.FindByName((string) gameSpinner.SelectedItem);

                    if (game == null) Toast.MakeText(Context, "The game was not found.", ToastLength.Short).Show();
                    else
                    {
                        // Create a new prescription object with the
                        // selected exercise name, RePlayGame,
                        // device name and time duration as parameters

                        int value = values[timeNumberPicker.Value];

                        Console.WriteLine("number value: " + value);
                        Prescription p = new Prescription(exerciseSpinner.SelectedItem.ToString(), game,
                                                          DevicesList[0], value);
                        PrescriptionManager.Instance.RemoveAt(PrescriptionManager.Instance.Count-1);
                        PrescriptionManager.Instance.Add(p);
                        PrescriptionManager.Instance.SavePrescription();
                        settingsActivity.NewPrescriptionAdded();
                    }
                    Dismiss();
                };

                builder.SetView(dialogView);
            }

            //Create the builder
            var dialog = builder.Create();

            //Now return the constructed dialog to the calling activity
            return dialog;
        }

        // Event handler for the cancel button
        void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog)sender;
            dialog.Dismiss();
        }

        // Event handler for the cance button 
        void CancelButton_Click(object sender, System.EventArgs e)
        {
            Dismiss();
        }
    }
}