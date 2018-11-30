using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using RePlay.Manager;
using RePlay.Entity;
using RePlay.Activities;

namespace RePlay.Fragments
{
    public class AddPrescriptionFragment : DialogFragment
    {
        readonly SettingsActivity settingsActivity;
        readonly List<string> GamesList;
        readonly List<string> ExercisesList;
        readonly List<string> DevicesList = new List<string>() { "FitMi", "Knob sensor" };
        readonly List<int> TimeList = new List<int>() { 1, 2, 3 };
        const int MAX_TIME = 15;
        const int MIN_TIME = 1;

        public AddPrescriptionFragment(SettingsActivity settingsActivity)
        {
            this.settingsActivity = settingsActivity;
            GamesList = GameManager.Instance.GetNames();
            ExercisesList = new List<string>(ExerciseManager.Instance.Keys);
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
                Spinner gameSpinner = dialogView.FindViewById<Spinner>(Resource.Id.gameSpinner);
                Spinner exerciseSpinner = dialogView.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                NumberPicker timeNumberPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.timeNumberPicker);

                timeNumberPicker.MinValue = MIN_TIME;
                timeNumberPicker.MaxValue = MAX_TIME;
                timeNumberPicker.Value = MIN_TIME;
                timeNumberPicker.WrapSelectorWheel = false;

                gameSpinner.Adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, GamesList);
                exerciseSpinner.Adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, ExercisesList);

                Button cancelButton = dialogView.FindViewById<Button>(Resource.Id.cancelButton);
                Button addButton = dialogView.FindViewById<Button>(Resource.Id.addButton);

                cancelButton.Click += CancelButton_Click;
                addButton.Click += (sender, args) =>
                {
                    RePlayGame game = GameManager.Instance.FindByName((string) gameSpinner.SelectedItem);

                    if (game == null) Toast.MakeText(Context, "The game was not found.", ToastLength.Short).Show();
                    else
                    {
                        Prescription p = new Prescription(exerciseSpinner.SelectedItem.ToString(), game,
                                                          DevicesList[0], timeNumberPicker.Value);

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

        void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog)sender;
            dialog.Dismiss();
        }

        void CancelButton_Click(object sender, System.EventArgs e)
        {
            Dismiss();
        }
    }
}