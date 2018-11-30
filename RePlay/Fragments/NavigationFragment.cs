using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using RePlay.Activities;

namespace RePlay.Fragments
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
            if (!button.Context.GetType().Equals(typeof(GamesListActivity))) {
                Intent intent = new Intent(button.Context, typeof(GamesListActivity));
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
            if (!button.Context.GetType().Equals(typeof(SettingsLoginActivity)) && !button.Context.GetType().Equals(typeof(SettingsActivity)))
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
            connectionButton.Click += ConnectionClicked;

            view.FindViewById<ImageButton>(Resource.Id.settingsButton).Click += SettingsClicked;

            return view;
        }
    }
}
