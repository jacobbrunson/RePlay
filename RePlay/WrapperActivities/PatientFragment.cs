
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RePlay.WrapperActivities
{
    public class PatientFragment : DialogFragment
    {
        static string PFirst;
        static string PLast;
        
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


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            View rootView = inflater.Inflate(Resource.Layout.PatientFragment, container, false);
            EditText First = rootView.FindViewById<EditText>(Resource.Id.patient_fname);
            EditText Last = rootView.FindViewById<EditText>(Resource.Id.patient_lname);
            First.Text = PFirst;
            Last.Text = PLast;

            return rootView;
        }
    }
}
