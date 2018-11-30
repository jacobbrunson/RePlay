using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace RePlay.Fragments
{
    // Fragment to update patient details and patient photo
    public class PatientFragment : DialogFragment
    {
        static string PFirst;
        static string PLast;
        public static event EventHandler<string> DialogClosed;

        // Return an instance of this fragment with the first and last fields set
        public static PatientFragment NewInstance(string name)
        {
            PatientFragment PatientFragmentInstance = new PatientFragment();
            if(name.Contains(' ')){
                string[] splitted = name.Split(' ');
                PFirst = splitted[0];
                PLast = splitted[1];
            }
            else{
                PFirst = name;
                PLast = "";
            }
            return PatientFragmentInstance;
        }


        // Default OnCreate method
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }


        // Inflates the PatientFragment View, instantiates the EditText fields for updating the names,
        // and instantiates a cancel and save button
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            Dialog.SetCanceledOnTouchOutside(false);
            View rootView = inflater.Inflate(Resource.Layout.PatientFragment, container, false);
            EditText First = rootView.FindViewById<EditText>(Resource.Id.patient_fname);
            EditText Last = rootView.FindViewById<EditText>(Resource.Id.patient_lname);
            First.Text = PFirst;
            Last.Text = PLast;

            ImageButton Cancel = rootView.FindViewById<ImageButton>(Resource.Id.patient_cancel);
            ImageButton Save = rootView.FindViewById<ImageButton>(Resource.Id.patient_save);

            Cancel.Click += (sender, args) =>
            {
                Dismiss();
            };

            Save.Click += (sender, args) =>
            {
                PFirst = First.Text;
                PLast = Last.Text;
                Dismiss();
            };

            return rootView;
        }

        // Dismisses this fragment and invokes the DialogClosed event
        // with the patient name passed as a parameter.
        // SettingsActivity is responsible for attaching an actual event
        // handler to the DialogClosed event.
        // That method sets the text of SettingsActivity's PatientName variable 
        // as the `name` passed into the delegate.
        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);
            string name = PFirst + " " + PLast;
            DialogClosed?.Invoke(this, name);
        }
    }
}
