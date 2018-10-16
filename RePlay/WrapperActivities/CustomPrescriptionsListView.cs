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
        private List<Prescription> PrescriptionsList;

        public CustomPrescriptionsListView(Context mcontext, List<Prescription> prescriptions)
        {
            Context = mcontext;
            PrescriptionsList = prescriptions;
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
                view = LayoutInflater.From(Context).Inflate(Resource.Layout.CustomGamesLauncher, null, false);
            }

//            ImageView GameView = view.FindViewById<ImageView>(Resource.Id.gameslist_image);
            //GameView.SetImageResource(GamesList[position].Image);

//            TextView GameText = view.FindViewById<TextView>(Resource.Id.gameslist_name);
//            GameText.Text = GamesList[position].Name;
//
            return view;
        }
    }

    public class Prescription
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

        public Prescription(int img, string game, string exercise, string device, int time)
        {
            Image = img;
            Game = game;
            Exercise = exercise;
            Device = device;
            Time = time;
        }
    }
}