using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using RePlay.Manager;
using RePlay.Entity;

namespace RePlay.Fragments
{
    public partial class SettingsActivity : Activity
    {
        public class AddPrescriptionFragment : DialogFragment
        {
            SettingsActivity settingsActivity;

            public AddPrescriptionFragment(SettingsActivity settingsActivity)
            {
                this.settingsActivity = settingsActivity;
            }

            public static AddPrescriptionFragment NewInstance(SettingsActivity settingsActivity)
            {
                var dialogFragment = new AddPrescriptionFragment(settingsActivity);
                return dialogFragment;
            }

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
                    var gamesList = GameManager.Instance.GetNames();
                    var exerciseList = new List<string>(ExerciseManager.Instance.Keys);
                    var deviceList = new List<string>() { "FitMi", "Knob sensor" };
                    var timeList = new List<int>() { 1, 2, 3 };

                    var gameSpinner = dialogView.FindViewById<Spinner>(Resource.Id.gameSpinner);
                    var exerciseSpinner = dialogView.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                    var timeNumberPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.timeNumberPicker);

                    timeNumberPicker.MinValue = 1;
                    timeNumberPicker.MaxValue = 15;
                    timeNumberPicker.Value = 1;
                    timeNumberPicker.WrapSelectorWheel = false;

                    var gameAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, gamesList);
                    var exerciseAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, exerciseList);

                    gameSpinner.Adapter = gameAdapter;
                    exerciseSpinner.Adapter = exerciseAdapter;

                    var cancelButton = dialogView.FindViewById<Button>(Resource.Id.cancelButton);
                    var addButton = dialogView.FindViewById<Button>(Resource.Id.addButton);

                    cancelButton.Click += (sender, args) =>
                    {
                        Dismiss();
                    };

                    addButton.Click += (sender, args) =>
                    {
                        var _dialog = dialogView;
                        var _exerciseSpinner = _dialog.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                        var _gameSpinner = _dialog.FindViewById<Spinner>(Resource.Id.gameSpinner);
                        var _timeNumberPicker = _dialog.FindViewById<NumberPicker>(Resource.Id.timeNumberPicker);

                        var prescriptionManager = PrescriptionManager.Instance;
                        var gameManager = GameManager.Instance;
                        RePlayGame game = gameManager.FindByName((string)_gameSpinner.SelectedItem);
                        if (game == null)
                        {
                            Toast.MakeText(Context, "The game was not found.", ToastLength.Short);
                        }
                        else
                        {
                            Prescription p = new Prescription(
                                (string)_exerciseSpinner.SelectedItem,
                                game,
                                null,
                                _timeNumberPicker.Value
                            );
                            prescriptionManager.Add(p);
                            /*
                            if ((prescriptionManager.Count + 1) % ItemsPerPage == 1) //+1 to account for last dummy element
                            {
                                settingsActivity.ACurrentPage += 1;
                            }
                            settingsActivity.assigned_paginator = new Paginator<Prescription>(ItemsPerPage, prescriptionManager);
                            settingsActivity.AssignedView.Adapter = new CustomPrescriptionsListView(
                                settingsActivity,
                                settingsActivity.assigned_paginator.GeneratePage(settingsActivity.ACurrentPage),
                                settingsActivity.assigned_paginator.ContainsLast(settingsActivity.ACurrentPage));
                            prescriptionManager.SavePrescription();
                            */
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

            void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
            {
                var dialog = (AlertDialog)sender;
                dialog.Dismiss();
            }
        }
    }
}