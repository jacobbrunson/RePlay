using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using RePlay.Manager;
using RePlay.Entity;
using RePlay.Activities;
using RePlay.DataClasses;
using System.Linq;

namespace RePlay.Fragments
{
    // Class for add presription dialog that
    // pops open when the therapist adds a
    // new prescription the settings page.
    public class EditPrescriptionFragment : DialogFragment
    {
        readonly SettingsActivity settingsActivity;
        readonly List<string> GamesList;
        readonly List<string> ExercisesList;
        readonly List<string> DevicesList = new List<string>() { "FitMi", "Knob sensor" };
        readonly List<int> TimeList = new List<int>() { 1, 2, 3 };
        const int MAX_TIME = 15;
        const int MIN_TIME = 1;
        readonly int PrescriptionPosition;
        readonly Prescription PrescriptionToEdit;

        // Constructor for AddPrescriptionFragment
        // Takes an argument for an instance to
        // SettingsActivity to be able to reference
        // SettingsActivity's variables
        public EditPrescriptionFragment(SettingsActivity settingsActivity, EditPrescriptionEventArgs e)
        {
            this.settingsActivity = settingsActivity;
            PrescriptionPosition = e.Position;
            PrescriptionToEdit = PrescriptionManager.Instance[PrescriptionPosition];
            GamesList = GameManager.Instance.GetNames();
            ExercisesList = new List<string>(ExerciseManager.Instance.Keys);
        }

        // Return a new instance of this class
        public static EditPrescriptionFragment NewInstance(SettingsActivity settingsActivity, EditPrescriptionEventArgs e)
        {
            var dialogFragment = new EditPrescriptionFragment(settingsActivity, e);
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
            var dialogView = inflater.Inflate(Resource.Layout.EditPrescriptionFragment, null);

            if (dialogView != null)
            {
                // Initially set the selected items of the
                // spinners and pickers based on what the
                // prescription's current values are.

                // Find the fragment element,
                // get the adapter containing the items that
                // can be selected, get the index of the item
                // in the spinner that matches the prescription's
                // current game name, and set that index as the
                // selected item of the spinner.
                Spinner gameSpinner = dialogView.FindViewById<Spinner>(Resource.Id.gameSpinner);
                gameSpinner.Adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, GamesList);
                System.Console.WriteLine(PrescriptionToEdit.Game.Name);
                IEnumerable<int> gameIndex = Enumerable.Range(0, gameSpinner.Adapter.Count).
                                                        Where((_, index) => (string)gameSpinner.Adapter.GetItem(index) == PrescriptionToEdit.Game.Name);
                // Note, the gameIndex query returns a collection,
                // but since there's only going to be one element
                // in it (the one that matches the prescription's
                // current game name), we take the first element
                // of that collection -- the index of that game
                // name -- to be the selected item
                gameSpinner.SetSelection(gameIndex.ElementAt(0));

                // We do the same thing for the other spinners
                // and the number picker.

                Spinner exerciseSpinner = dialogView.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                exerciseSpinner.Adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, ExercisesList);
                IEnumerable<int> exerciseIndex = Enumerable.Range(0, exerciseSpinner.Adapter.Count).
                                                 Where((_, index) => (string)exerciseSpinner.Adapter.GetItem(index) == PrescriptionToEdit.Exercise);
                gameSpinner.SetSelection(gameIndex.ElementAt(0));

                NumberPicker timeNumberPicker = dialogView.FindViewById<NumberPicker>(Resource.Id.timeNumberPicker);
                timeNumberPicker.MinValue = MIN_TIME;
                timeNumberPicker.MaxValue = MAX_TIME;
                IEnumerable<int> timeIndex = Enumerable.Range(timeNumberPicker.MinValue, timeNumberPicker.MaxValue).
                                             Where((num, _) => num == PrescriptionToEdit.Duration);
                timeNumberPicker.Value = timeIndex.ElementAt(0);
                timeNumberPicker.WrapSelectorWheel = false;

                Button cancelButton = dialogView.FindViewById<Button>(Resource.Id.cancelButton);
                Button saveButton = dialogView.FindViewById<Button>(Resource.Id.saveButton);

                cancelButton.Click += CancelButton_Click;
                // We add an anonymous method to the
                // `addButton.Click` delegate
                saveButton.Click += (sender, args) =>
                {
                    // Retrive the actual RePlayGame object
                    // with the same game name as the one the
                    // user picked.
                    PrescriptionToEdit.Game = GameManager.Instance.FindByName((string) gameSpinner.SelectedItem);
                    PrescriptionToEdit.Duration = timeNumberPicker.Value;
                    PrescriptionToEdit.Exercise = exerciseSpinner.SelectedItem.ToString();

                    settingsActivity.RefreshAssignedPrescriptions();

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