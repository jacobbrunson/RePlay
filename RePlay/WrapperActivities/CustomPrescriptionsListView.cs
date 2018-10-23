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
    [Activity(Label = "CustomPrescriptionsListView")]
    public class CustomPrescriptionsListView : BaseAdapter
    {
        private Context Context;
        private List<SettingsPrescription> PrescriptionsList;
        private bool ContainsLast;
        private bool isAssigned;
        public delegate void AddEventsToCard(View view, string flag);
        public event AddEventsToCard EventAdder;

        public CustomPrescriptionsListView(Context mcontext, List<SettingsPrescription> prescriptions, bool hasLastElement, AddEventsToCard eventAdder):
            this(mcontext, prescriptions, hasLastElement)
        {
            this.EventAdder += new AddEventsToCard(eventAdder);
        }

        public CustomPrescriptionsListView(Context mcontext, List<SettingsPrescription> prescriptions, bool hasLastElement)
        {
            Context = mcontext;
            PrescriptionsList = prescriptions;
            ContainsLast = hasLastElement;
            isAssigned = true;
        }

        public CustomPrescriptionsListView(Context mcontext, List<SettingsPrescription> prescriptions)
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
                if (ContainsLast == true &&  position == PrescriptionsList.Count - 1)
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.PrescriptionsGridPlus, null, false);
                }
                else if (isAssigned)
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.PrescriptionsGrid, null, false);
                }
                else
                {
                    view = LayoutInflater.From(Context).Inflate(Resource.Layout.SavedPrescription, null, false);
                }
            }
            {
                System.Console.WriteLine("Doing something.");
                EventAdder?.Invoke(view, "last");
                System.Console.WriteLine("Doing something again.");
            }
            return view;
        }
    }

    public class SettingsPrescription
    {

        public int Image
        {
            get;
            set;
        }

        public string Exercise
        {
            get;
            set;
        }

        public string Game
        {
            get;
            set;
        }

        public string Device
        {
            get;
            set;
        }

        public int Time
        {
            get;
            set;
        }

        public SettingsPrescription(int img, string game, string exercise, string device, int time)
        {
            Image = img;
            Game = game;
            Exercise = exercise;
            Device = device;
            Time = time;
        }
    }
}