using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using RePlay.Entity;

namespace RePlay.Fragments
{
    // Fragment to update patient details and patient photo
    public class PatientFragment : DialogFragment
    {
        static Patient patient;
        static ImageView photoView;

        public static event EventHandler<Patient> DialogClosed;

        // Return an instance of this fragment with the patient fields set
        public static PatientFragment NewInstance(Patient patient)
        {
            PatientFragment PatientFragmentInstance = new PatientFragment();
            PatientFragment.patient = patient;
            return PatientFragmentInstance;
        }


        // Default OnCreate
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
            First.Text = patient.First;
            Last.Text = patient.Last;

            ImageButton Cancel = rootView.FindViewById<ImageButton>(Resource.Id.patient_cancel);
            ImageButton Save = rootView.FindViewById<ImageButton>(Resource.Id.patient_save);
            ImageButton Camera = rootView.FindViewById<ImageButton>(Resource.Id.patient_camera);
            photoView = rootView.FindViewById<ImageView>(Resource.Id.patient_image);
            photoView.SetImageBitmap(patient.Photo);
            
            Cancel.Click += (sender, args) =>
            {
                Dismiss();
            };

            Save.Click += (sender, args) =>
            {
                patient.First = First.Text;
                patient.Last = Last.Text;
                Dismiss();
            };

            Camera.Click += (sender, args) =>
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            };

            return rootView;
        }

        //This will be called after taking an image with the camera
        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            patient.Photo = (Bitmap)data.Extras.Get("data");
            photoView.SetImageBitmap(patient.Photo);
        }

        // Dismisses this fragment and invokes the DialogClosed event
        // with the Patient (fname, lname, photo) passed as a parameter.
        // SettingsActivity is responsible for attaching an actual event
        // handler to the DialogClosed event.
        // That method sets the text of SettingsActivity's PatientName variable 
        // as the `name` passed into the delegate. It also sets the Photo.
        public override void OnDismiss(IDialogInterface dialog)
        {
            base.OnDismiss(dialog);
            DialogClosed?.Invoke(this, patient);
        }
    }
}
