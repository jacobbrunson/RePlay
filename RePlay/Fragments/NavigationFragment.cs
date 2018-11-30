using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using RePlay.Activities;

namespace RePlay.Fragments
{
    // Top navigation bar for going to the differnt activities
    // of the RePlay app (i.e. games, settings, home screen)
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

        // Method that starts GamesListActivity if the user
        // is not already on the games list screen.
        // This method is added to the Click delegate of the
        // game button when OnCreateView is called
        void GamesClicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            if (!button.Context.GetType().Equals(typeof(GamesListActivity))) {
                Intent intent = new Intent(button.Context, typeof(GamesListActivity));
                StartActivity(intent);
            }
        }

        // Stub method that will later be used to indicate
        // the Bluetooth connection status of the FitMi
        // device to the VNS chip implanted in the patient
        void ConnectionClicked(object sender, EventArgs e)
        {
            IsConnected = !IsConnected;
        }

        // Method that starts SettingsLoginActivity if the user
        // is not already on the settings login screen.
        // This method is added to the Click delegate of the
        // settings button when OnCreateView is called
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
        // Boolean property that sets the appropriate
        // picture for `connectionButton` based on 
        // whether the Bluetooth connection is active
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


        // Default OnCreate
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        // Inflates the Navigation view and adds the resepective event
        // handlers to the Click delegate of each button
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
