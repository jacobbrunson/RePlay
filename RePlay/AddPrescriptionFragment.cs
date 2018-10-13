using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using System;

namespace RePlay
{
    public class AddPrescriptionFragment : DialogFragment
    {
        public class DialogEventArgs : EventArgs
        {
            public string Exercise { get; set; }
            public string Game { get; set; }
            public string Device { get; set; }
            public int Time { get; set; }
        }

        public delegate void DialogEventHandler(object sender, DialogEventArgs args);
        public event DialogEventHandler Dismissed;

        //Create class properties
        protected EditText NameEditText;
        protected EditText DescriptionEditText;
        protected EditText PriceEditText;
        protected EditText AddCategoryEditText;
        protected Spinner CategorySpinner;
        protected LinearLayout CategoryLayout;
        protected CheckBox CategoryCheckBox;
        protected Button CategoryButton;

        //Create the string that will hold the value
        //Of the category drop down selected item
        protected string SelectedCategory = "";

        /// <summary>
        /// Method that creates and returns and instance of this dialog
        /// </summary>
        /// <returns></returns>
        public static AddPrescriptionFragment NewInstance()
        {
            var dialogFragment = new AddPrescriptionFragment();
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
            var dialogView = inflater.Inflate(Resource.Layout.AddPrescription, null);

            if (dialogView != null)
            {
                var gamesList = new List<string>() { "Breakout", "Crossy Road", "Handwriting"};
                var exerciseList = new List<string>() { "Wrist flexion", "Bicep curl", "Thumb press" };
                var deviceList = new List<string>() { "FitMi", "Knob sensor"};
                var timeList = new List<int>() { 1, 2, 3 };

                var gameSpinner = dialogView.FindViewById<Spinner>(Resource.Id.gameSpinner);
                var exerciseSpinner = dialogView.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                var deviceSpinner = dialogView.FindViewById<Spinner>(Resource.Id.deviceSpinner);
                var timeSpinner = dialogView.FindViewById<Spinner>(Resource.Id.timeSpinner);

                var gameAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, gamesList);
                var exerciseAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, exerciseList);
                var deviceAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, deviceList);
                var timeAdapter = new ArrayAdapter<int>(Context, Android.Resource.Layout.SimpleSpinnerItem, timeList);

                gameSpinner.Adapter = gameAdapter;
                exerciseSpinner.Adapter = exerciseAdapter;
                deviceSpinner.Adapter = deviceAdapter;
                timeSpinner.Adapter = timeAdapter;

                var cancelButton = dialogView.FindViewById<Button>(Resource.Id.cancelButton);
                var addButton = dialogView.FindViewById<Button>(Resource.Id.addButton);

                cancelButton.Click += (sender, args) =>
                {
                    Dismiss();
                };

                addButton.Click += (sender, args) =>
                {
                    var _dialog = dialogView;
                    var _exerciseSpinner = (Spinner)_dialog.FindViewById<Spinner>(Resource.Id.exerciseSpinner);
                    var _gameSpinner = (Spinner)_dialog.FindViewById(Resource.Id.gameSpinner);
                    var _deviceSpinner = (Spinner)_dialog.FindViewById<Spinner>(Resource.Id.deviceSpinner);
                    var _timeSpinner = (Spinner)_dialog.FindViewById<Spinner>(Resource.Id.timeSpinner);

                    Dismissed?.Invoke(this, new DialogEventArgs
                    {
                        Exercise = (string)exerciseSpinner.SelectedItem,
                        Game = (string)gameSpinner.SelectedItem,
                        Device = (string)deviceSpinner.SelectedItem,
                        Time = (int)timeSpinner.SelectedItem
                    });
                    Dismiss();
                };

                builder.SetView(dialogView);
            }

            //Create the builder 
            var dialog = builder.Create();

            //Now return the constructed dialog to the calling activity
            return dialog;
        }

        private void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog)sender;
            dialog.Dismiss();
        }
    }
}