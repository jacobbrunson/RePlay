
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

namespace RePlay
{
    public class NavigationFragment : Fragment
    {
        void HomeClicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (!button.Context.GetType().Equals(typeof(MainActivity)))
            {
                Intent intent = new Intent(button.Context, typeof(MainActivity));
                button.Context.StartActivity(intent);
            }
        }

        void GamesClicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (!button.Context.GetType().Equals(typeof(WrapperActivities.GamesListActivity))) {
                Intent intent = new Intent(button.Context, typeof(WrapperActivities.GamesListActivity));
                StartActivity(intent);
            }
        }

        void ConnectionClicked(object sender, EventArgs e)
        {
            IsConnected = !IsConnected;
        }

        void SettingsClicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (!button.Context.GetType().Equals(typeof(SettingsLoginActivity)) && !button.Context.GetType().Equals(typeof(WrapperActivities.SettingsActivity)))
            {
                Intent intent = new Intent(button.Context, typeof(SettingsLoginActivity));
                StartActivity(intent);
            }
        }

        bool isConnected = true;
        public bool IsConnected {
            get { return isConnected; }
            set
            {
                isConnected = value;
                if (isConnected) {
                    connectionButton.SetImageResource(Resource.Drawable.pcmConnected);
                } else {
                    connectionButton.SetImageResource(Resource.Drawable.pcmDisconnected);
                }
            }
        }

        ImageButton connectionButton;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Navigation, container, false);

            view.FindViewById<ImageButton>(Resource.Id.homeButton).Click += HomeClicked;
            view.FindViewById<ImageButton>(Resource.Id.gamesButton).Click += GamesClicked;

            connectionButton = view.FindViewById<ImageButton>(Resource.Id.connectionButton);
            connectionButton.Click += connectionClicked;

            view.FindViewById<ImageButton>(Resource.Id.settingsButton).Click += SettingsClicked;

            return view;
        }
    }
}
