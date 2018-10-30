using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace RePlay.WrapperActivities
{
    public partial class SettingsActivity : Activity
    {
        public class CustomPrescriptionsListView : BaseAdapter
        {
            List<Prescription> PrescriptionsList;
            readonly Context Context;
            readonly bool ContainsLast;
            readonly bool isAssigned;
            readonly SettingsActivity settingsActivity;

            public CustomPrescriptionsListView(Context mcontext, List<Prescription> prescriptions, bool hasLastElement)
            {
                Context = mcontext;
                PrescriptionsList = prescriptions;
                ContainsLast = hasLastElement;
                isAssigned = true;
                settingsActivity = (SettingsActivity)mcontext;
            }

            public CustomPrescriptionsListView(Context mcontext, List<Prescription> prescriptions)
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
                        //GameText.Text = card.Game.Name;
                    }
                    else
                    {
                        view = LayoutInflater.From(Context).Inflate(Resource.Layout.SavedPrescription, null, false);
                    }
                }

                return view;
            }

            void Add_Prescription_Click(object sender, EventArgs e)
            {
                Activity settings = (Activity)Context;
                FragmentTransaction fm = settings.FragmentManager.BeginTransaction();
                AddPrescriptionFragment dialog = AddPrescriptionFragment.NewInstance(settingsActivity);
                dialog.Show(fm, "dialog fragment");
            }
        }
    }
}